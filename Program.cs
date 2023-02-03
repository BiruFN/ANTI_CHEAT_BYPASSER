using System;
using System.Diagnostics;

namespace ANTI_CHEAT_BYPASSER
{
    internal class Program
    {
        static string caldera = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJhY2NvdW50X2lkIjoiIiwiZ2VuZXJhdGVkIjoxNjc1MTY5ODQ2LCJjYWxkZXJhR3VpZCI6IjIwZDg3N2JjLTc1MGUtNGY3Ni1hZjIxLTA3NjUxMmVjOTMyNCIsImFjUHJvdmlkZXIiOiJFYXN5QW50aUNoZWF0Iiwibm90ZXMiOiJkb1JlcXVlc3QgZXJyb3I6IGRvUmVxdWVzdCBmYWlsdXJlIGNvZGU6IDQwMCIsImZhbGxiYWNrIjp0cnVlfQ.npqgHriv6k1PIf34mcc0yFV0MBVAM2k5bZKZ7SA6uQ4";
        static void Main(string[] args)
        {
            Console.Title = "ANTI CHEAT BYPASSER";
            Console.WriteLine("[=] ANTI CHEAT BYPASSER made by BiruFN#0746");
            var fnPath = FNInstalled.GetFNPath();
            var fnVersion = FNInstalled.GetFNVersion();

            Console.WriteLine($"[+] FNPath: {fnPath}");
            Console.WriteLine($"[+] FNVersion: {fnVersion}");

            Console.WriteLine("[?] Enter your PCGameClient AuthCode!");
            var code = Console.ReadLine();

            var token = Auth.GetToken(code);
            var exchange = Auth.GetExchange(token);

            Console.WriteLine("[+] Start fortnite!");

            var process = new Process
            {
                StartInfo =
                {
                    FileName = $@"{fnPath}\FortniteGame\Binaries\Win64\FortniteClient-Win64-Shipping.exe",
                    Arguments = $"-AUTH_LOGIN=unused -AUTH_PASSWORD={exchange} -AUTH_TYPE=exchangecode -epicapp=Fortnite -epicenv=Prod -EpicPortal -nobe -fromfl=eac -caldera={caldera}",
                    WorkingDirectory = $@"{fnPath}\FortniteGame\Binaries\Win64",
                    UseShellExecute = false,
                    RedirectStandardOutput = true
                }
            };
            process.Start();

            var process2 = new Process
            {
                StartInfo =
                {
                    FileName = $@"{fnPath}\FortniteGame\Binaries\Win64\FortniteClient-Win64-Shipping_EAC.exe",
                    Arguments = $"-AUTH_LOGIN=unused -AUTH_PASSWORD={exchange} -AUTH_TYPE=exchangecode -epicapp=Fortnite -epicenv=Prod -EpicPortal -nobe -fromfl=eac -caldera={caldera}",
                    WorkingDirectory = $@"{fnPath}\FortniteGame\Binaries\Win64",
                    UseShellExecute = false,
                    CreateNoWindow= true
                }
            };
            process2.Start();
            foreach (ProcessThread thread in process2.Threads)
            {
                Win32.SuspendThread(Win32.OpenThread(0x0002, false, thread.Id));
            }

            var process3 = new Process
            {
                StartInfo =
                {
                    FileName = $@"{fnPath}\FortniteGame\Binaries\Win64\FortniteLauncher.exe",
                    Arguments = $"-AUTH_LOGIN=unused -AUTH_PASSWORD={exchange} -AUTH_TYPE=exchangecode -epicapp=Fortnite -epicenv=Prod -EpicPortal",
                    WorkingDirectory = $@"{fnPath}\FortniteGame\Binaries\Win64",
                    UseShellExecute = false,
                    CreateNoWindow= true
                }
            };
            process3.Start();
            foreach (ProcessThread thread in process3.Threads)
            {
                Win32.SuspendThread(Win32.OpenThread(0x0002, false, thread.Id));
            }

            AsyncStreamReader asyncStreamReader = new AsyncStreamReader(process.StandardOutput);
            asyncStreamReader.DataReceived += delegate (object sender, string data)
            {
                Console.WriteLine(data);
            };
            asyncStreamReader.Start();

            process.WaitForExit();
            process2.Kill();
            process3.Kill();
        }
    }
}
