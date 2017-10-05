using System; using System.IO; using OpenQA.Selenium; using OpenQA.Selenium.Chrome; using System.Threading; using System.Diagnostics; using OpenQA.Selenium.Support.UI; using Microsoft.Extensions.Configuration;

namespace Stress_Console {
	class MainClass
	{
		const int timeoutSec = 20;
		//const string seanUrl = "https://checed.botsnova.com/";
		//const string nissanUrl = "http://new.nissan.com.tw/nissan/";
		static readonly string path = Directory.GetCurrentDirectory();
        static IConfigurationRoot Configuration; 
		public static void Main(string[] args)
		{
			try
            {
				var builder = new ConfigurationBuilder()
        		.SetBasePath(Directory.GetCurrentDirectory())
        		.AddJsonFile("appsettings.json");
				Configuration = builder.Build(); 
                int attackTimes;
                Console.Write("How many 'Times' do you want to attack?(Default: {0} times) ",Configuration["defaultAttackTimes"]);
                var attackTimesTmp = Console.ReadLine();
				if (!int.TryParse(attackTimesTmp, out attackTimes) || string.IsNullOrEmpty(attackTimesTmp))
					attackTimes = int.Parse(Configuration["defaultAttackTimes"]);
				Console.WriteLine(attackTimes + " times\n");

				int threadCount;
                Console.Write("How many 'Threads' do you want to create?(Default: {0} threads) ",Configuration["defaultThreadCount"]);
				var threadCountTmp = Console.ReadLine();
				if (!int.TryParse(threadCountTmp, out threadCount) || string.IsNullOrEmpty(threadCountTmp))
					threadCount = int.Parse(Configuration["defaultThreadCount"]);
                Console.WriteLine(threadCount + " threads\n");


                Console.Write("Which website do you want to Attack?(Default: {0}) ",Configuration["defaultExecutionUrl"]);
				string exeUrlTmp = Console.ReadLine();
				string exeUrl = string.IsNullOrEmpty(exeUrlTmp) ? Configuration["defaultExecutionUrl"] : exeUrlTmp;
				Console.WriteLine("We're going to attack " + exeUrl + "!!\n");

				int suspendSeconds;
                Console.Write("How many seconds do you want to suspend in each attack?(Default: {0} second) ",Configuration["defaultSuspendSeconds"]);
				string suspendSecondsTmp = Console.ReadLine();
				if (!int.TryParse(suspendSecondsTmp, out suspendSeconds) || string.IsNullOrEmpty(suspendSecondsTmp))
					suspendSeconds = int.Parse(Configuration["defaultSuspendSeconds"]);
				Console.WriteLine(suspendSeconds + " seconds\n");

				Stopwatch sw = new Stopwatch();
				sw.Reset();
				sw.Start();
                 Console.WriteLine("Start at: {0}", DateTime.Now.ToString());
				Thread[] workers = new Thread[threadCount];
				int jobsCountPerWorker = attackTimes / threadCount;                  //using (IWebDriver driver = new ChromeDriver(path))                 //            {                 //    string existingWindowHandle = driver.CurrentWindowHandle;                 //    var driver2 = driver;                 //                driver.Url = exeUrl;                 //                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutSec));                 //                IWebElement element = wait.Until(x => x.FindElement(By.ClassName("toolbar")));                 //                IWebElement myDynamicElement = wait.Until<IWebElement>(d => d.FindElement(By.Id("UradTracking")));                 //                //var test = driver.FindElement(By.Id("fb-root"));                 //                Console.WriteLine("attacked!");                 //}             
				for (int i = 0; i < threadCount; i++)
				{
					int st = jobsCountPerWorker * i;
					int ed = jobsCountPerWorker * (i + 1);
					if (ed > attackTimes) ed = attackTimes;
					workers[i] = new Thread(() =>
					{
                        for (int j = st; j < ed; j++)
                        {
                            /// Start to attack
                            using (IWebDriver driver = new ChromeDriver(path))
                            {                                 driver.Url = exeUrl;
                                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutSec));
                                //IWebElement element = wait.Until(x => x.FindElement(By.ClassName("toolbar")));
                                //IWebElement myDynamicElement = wait.Until<IWebElement>(d => d.FindElement(By.Id("UradTracking")));
                                Console.WriteLine("No." + j + " attacked!");
                                Thread.Sleep(suspendSeconds * 1000);
                            }
                        }

					});
					workers[i].Start();
				}
				for (int i = 0; i < threadCount; i++)
					workers[i].Join();
				sw.Stop();                 Console.WriteLine("End at: {0}", DateTime.Now.ToString());
				Console.WriteLine("\nAttacked completely! Congratulations!!");
				decimal spentSeconds = decimal.Parse(sw.ElapsedMilliseconds.ToString());
				Console.WriteLine("This attack spent about {0} seconds.", Math.Round(spentSeconds / 1000, 1));
            }
			catch (Exception ex)
			{
				Console.WriteLine(ex + "You got an error!");
				Console.Read();
			}
		}
	} }  