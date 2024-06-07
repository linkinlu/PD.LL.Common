using System;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;

namespace PD.LL.Common
{
    public class ScriptManager
    {
        /// <summary>
        /// 动态执行脚本
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        /// <exception cref="CompilationErrorException"></exception>
        /// <exception cref="Exception"></exception>
        public static async Task<object> ExecuteScript(string expression)
        {
            
            //sample
            //string expression = @"  if (3 >2) {return 1 ;} else {return  0;}";//"2 + 3 * 4"; // 要执行的表达式
            // 创建脚本运行时
            var scriptOptions = ScriptOptions.Default;
            var script = CSharpScript.Create(expression, options: scriptOptions);
        
            try
            {
                // 执行脚本
                var result = await script.RunAsync();

                return result.ReturnValue;
            }
            catch (CompilationErrorException ex)
            {
                // 如果代码编译错误，则捕获并输出错误信息
                Console.WriteLine("Compilation Error: " + string.Join(Environment.NewLine, ex.Diagnostics));
                throw ex;
            }
            catch (Exception ex)
            {
                // 其他异常情况
                Console.WriteLine("Error: " + ex.Message);
                throw ex;
            }
        }

      

    }
}