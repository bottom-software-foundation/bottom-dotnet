using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.IO;
using System.Text;

namespace Bottom_NET.CLI
{
    class Program
    {
        static int Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            RootCommand rootCommand = new RootCommand
            {
                new Option<bool>(
                    new string[] {"-b", "--bottomify"},
                    description: "Translate text to bottom"),
               new Option<bool>(
                    new string[] {"-r", "--regress"},
                    description: "Translate bottom to human-readable text (futile)"),
                new Option<FileInfo>(
                    new string[] {"-i", "--input"},
                    description: "Input file [default: stdin]"),
               new Option<FileInfo>(
                    new string[] {"-o", "--output"},
                    description: "Output file [default: stdout]"),
               new Argument<string>(
                    "text",
                    getDefaultValue: () => null
               )
            };

            rootCommand.Description = "Fantastic (maybe) CLI for translating between bottom and human-readable text";

            rootCommand.Handler = CommandHandler.Create<bool, bool, FileInfo, FileInfo, string>((bottomify, regress, input, output, text) =>
            {
                if (!(bottomify || regress))
                {
                    rootCommand.InvokeAsync("--help");
                    return;
                }

                if (bottomify && regress)
                {
                    Console.Error.WriteLine("Both encoding options set, only set one.");
                    return;
                }

                if (input is null && text is null)
                {
                    Console.Error.WriteLine("Either input text or the --input options must be provided.");
                    return;
                }

                if (!(input is null)) {
                    if (!input.Exists)
                    {
                        Console.Error.WriteLine($"Input file \"{input.FullName}\" does not exist.");
                        return;
                    }
                    using StreamReader sr = input.OpenText();
                    text = sr.ReadToEnd();
                }

                string result;
                if (bottomify)
                {
                    result = Bottom.encode_string(text);
                }
                else
                {
                    result = Bottom.decode_string(text);
                }

                if (!(output is null))
                {
                    using StreamWriter sw = output.CreateText();
                    sw.Write(result);
                } 
                else
                {
                    Console.WriteLine(result);
                }

            });

            return rootCommand.InvokeAsync(args).Result;
        }
    }
}
