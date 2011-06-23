﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using IronJS;
using JSBuild;
using JSBuild.Utility;

namespace JSBuild.TaskMethods
{
    public class Exec : IBuildFunction
    {
        public string MethodName
        {
            get { return "exec"; }
        }

        public Func<BoxedValue, BoxedValue> GetFunction()
        {
            var func = (Func<BoxedValue, BoxedValue>) (
                options =>
                {
                    var executable = options.SimpleProperty<string>("Path");
                    var arguments = options.SimpleProperty<string>("Args");

                    var process = new Process();
                    process.StartInfo.FileName = executable;
                    process.StartInfo.Arguments = arguments;

                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.CreateNoWindow = true;
                    process.StartInfo.RedirectStandardInput = false;
                    process.StartInfo.RedirectStandardError = true;
                    process.StartInfo.RedirectStandardOutput = true;
                    process.EnableRaisingEvents = true;
                    process.OutputDataReceived += (sender, e) => { Console.ResetColor(); Console.WriteLine(e.Data); };
                    process.ErrorDataReceived += (sender, e) => { Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine(e.Data); };

                    process.Start();
                    process.BeginOutputReadLine();
                    process.BeginErrorReadLine();
                    process.WaitForExit();

                    var exitCode = Convert.ToDouble(process.ExitCode);
                    Console.WriteLine("PROCESS USER:");
                    Console.WriteLine(process.StartInfo.UserName);
                    
                    process.Close();

                    return TypeConverter.ToBoxedValue(exitCode);
                });

            return func;
        }
    }
}
