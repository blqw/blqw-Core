using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace blqw
{
    /// <summary> 动态编译
    /// </summary>
    public static class DynamicCompile
    {
        /// <summary>
        /// 编译类,并返回已编译类的的类型
        /// </summary>
        public static Type CompileClass(string code, params Type[] usingTypes)
        {
            var ass = CompileAssembly(code, usingTypes);
            return ass.GetTypes()[0];
        }
        /// <summary>
        /// 编译类,并返回已编译类的实例对象
        /// </summary>
        public static object CompileObject(string code, params Type[] usingTypes)
        {
            var ass = CompileAssembly(code, usingTypes);
            return ass.GetTypes()[0].GetConstructors()[0].Invoke(null);
        }

        //验证方法并获取方法名
        private static Regex Regex_Method = new Regex(@"^\s*[\sa-z_]*\s(?<n>[a-z_][a-z0-9_]*)[(](([a-z_][a-z_0-9]*\s+[a-z_][a-z_0-9]*\s*,\s*)*([a-z_][a-z_0-9]*\s+[a-z_][a-z_0-9]*\s*))?[)][\s]*[{][^}]*[}][\s]*\s*$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        //格式化用字符串
        private const string FORMATCALSSCODE = @"
public class %ClassName:ICloneable
{
    object ICloneable.Clone()
    {
        return (%Type)%MethodName;
    }
    %Method
}";
        /// <summary>
        /// 编译方法,并返回方法的委托
        /// </summary>
        /// <typeparam name="T">方法委托类型</typeparam>
        public static T CompileMethod<T>(string code, params Type[] usingTypes)
        {
            var m = Regex_Method.Match(code);//验证方法代码是否可以用
            if (m.Success == false)
            {
                throw new ArgumentException("code参数有误", "code");
            }
            code = FORMATCALSSCODE
                .Replace("%ClassName", "_" + Guid.NewGuid().ToString("N"))
                .Replace("%Type", GetTypeDisplayName(typeof(T)))
                .Replace("%MethodName", m.Groups["n"].Value)
                .Replace("%Method", code);
            var obj = CompileObject(code, usingTypes);
            return (T)((ICloneable)obj).Clone();
        }
        //获取类型的可视化名称
        static string GetTypeDisplayName(Type type)
        {
            if (type == null)
            {
                return "null";
            }
            if (type.IsGenericType)
            {
                var arr = type.GetGenericArguments();
                string gname = type.GetGenericTypeDefinition().FullName;
                gname = gname.Remove(gname.IndexOf('`'));
                if (arr.Length == 1)
                {
                    return gname + "<" + GetTypeDisplayName(arr[0]) + ">";
                }
                StringBuilder sb = new StringBuilder(gname);
                sb.Append("<");
                foreach (var a in arr)
                {
                    sb.Append(GetTypeDisplayName(a));
                    sb.Append(",");
                }
                sb[sb.Length - 1] = '>';
                return sb.ToString();
            }
            else
            {
                return type.FullName;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code">需要编译的C#代码</param>
        /// <param name="usingTypes">编译代码中需要引用的类型</param>
        /// <returns></returns>
        public static Assembly CompileAssembly(string code, params Type[] usingTypes)
        {
            CompilerParameters compilerParameters = new CompilerParameters();//动态编译中使用的参数对象
            compilerParameters.GenerateExecutable = false;//不需要生成可执行文件
            compilerParameters.GenerateInMemory = true;//直接在内存中运行
            compilerParameters.IncludeDebugInformation = false;
            //添加需要引用的类型
            Dictionary<string, bool> ns = new Dictionary<string, bool>();//用来保存命名空间,

            ns["using System;" + Environment.NewLine] = true;
            compilerParameters.ReferencedAssemblies.Add(typeof(int).Module.FullyQualifiedName);//这个相当于引入dll
            foreach (var type in usingTypes)
            {
                ns["using " + type.Namespace + ";" + Environment.NewLine] = true;//记录命名空间,不重复
                compilerParameters.ReferencedAssemblies.Add(type.Module.FullyQualifiedName);//这个相当于引入dll
            }

            string[] usings = new string[ns.Count];
            ns.Keys.CopyTo(usings, 0);
            code = string.Concat(usings) + code;//加入using命名空间的代码,即使原来已经有了也不会报错的

            //声明编译器
            using (CSharpCodeProvider objCSharpCodePrivoder = new CSharpCodeProvider())
            {
                //开始编译
                CompilerResults cr = objCSharpCodePrivoder.CompileAssemblyFromSource(compilerParameters, code);

                if (cr.Errors.HasErrors)//如果有错误
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("编译错误:");
                    foreach (CompilerError err in cr.Errors)
                    {
                        sb.AppendLine(err.ErrorText);
                    }
                    throw new Exception(sb.ToString());
                }
                else
                {
                    //返回已编译程序集
                    return cr.CompiledAssembly;
                }
            }
        }
    }
}