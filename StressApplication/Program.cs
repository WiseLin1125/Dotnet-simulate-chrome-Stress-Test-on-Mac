using System;
using System.IO;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;
using System.Diagnostics;
using OpenQA.Selenium.Support.UI;

namespace Stress_Console
{
	class MainClass
	{
		const int timeoutSec = 60;
		const string seanUrl = "https://checed.botsnova.com/";
		const string nissanUrl = "http://new.nissan.com.tw/nissan/";
        static readonly string path = Directory.GetCurrentDirectory();
		const int sleepSecond = 2;

		public static void Main(string[] args)
		{
            try
            {
                int attackTimes;
                Console.Write("How many 'Times' do you want to attack?(Default: 50 times)");
                var attackTimesRead= Console.ReadLine();
                if (!int.TryParse(attackTimesRead, out attackTimes) || string.IsNullOrEmpty(attackTimesRead))
                    attackTimes = 50;
                Console.WriteLine(attackTimes + " times\n");

				int threadCount;
                Console.Write("How many 'Threads' do you want to create?(Default: 5 threads)");
				var threadCountRead = Console.ReadLine();
				if (!int.TryParse(threadCountRead, out threadCount) || string.IsNullOrEmpty(threadCountRead))
					threadCount = 5;
				Console.WriteLine(threadCount + " threads\n");


				Console.Write("Which website do you want to Attack?(Default: https://checed.botsnova.com/)");
                string exeUrlTmp = Console.ReadLine();
                string exeUrl = string.IsNullOrEmpty(exeUrlTmp) ? "https://checed.botsnova.com/" : exeUrlTmp;
                Console.WriteLine("We're going to attack "+exeUrl+"!!\n");

                Stopwatch sw = new Stopwatch();
                sw.Reset();
                sw.Start();

                Thread[] workers = new Thread[threadCount];
                int jobsCountPerWorker = attackTimes / threadCount;
                for (int i = 0; i < threadCount; i++)
                {
                    int st = jobsCountPerWorker * i;
                    int ed = jobsCountPerWorker * (i + 1);
                    if (ed > attackTimes) ed = attackTimes;
                    workers[i] = new Thread(() =>
                    {
                        IWebDriver driver = new ChromeDriver(path);
                        for (int j = st; j < ed; j++)
                        {
                            Console.WriteLine("No." + j+" attacked!");
                            WebDriverWait wait = new WebDriverWait(driver, new TimeSpan(0, 0, timeoutSec));
                            driver.Navigate().GoToUrl(exeUrl);
                            Thread.Sleep(sleepSecond * 1000);
                        }
                        driver.Close();

                    });
                    workers[i].Start();
                }
                for (int i = 0; i < threadCount; i++)
                    workers[i].Join();

                Console.WriteLine("Attacked completely! Congratulations!!");
            }
			catch (Exception ex)
			{
				Console.WriteLine(ex + "You got an error!");
				Console.Read();
			}
		}
	}
}
