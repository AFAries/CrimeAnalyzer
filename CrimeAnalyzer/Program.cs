using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace CrimeAnalyzer
{

    public class Crime
    {
        public int Year;
        public int Population;
        public int ViolentCrime;
        public int Murder;
        public int Rape;
        public int Robbery;
        public int AggravatedAssault;
        public int PropertyCrime;
        public int Burglary;
        public int Theft;
        public int MotorVehicleTheft;


        public Crime(int year, int population, int violentCrime,
                int murder, int rape, int robbery, int aggravatedAssault,
                int propertyCrime, int burglary, int theft,
                   int motorVehicleTheft)
        {
            Year = year;
            Population = population;
            ViolentCrime = violentCrime;
            Murder = murder;
            Rape = rape;
            Robbery = robbery;
            AggravatedAssault = aggravatedAssault;
            PropertyCrime = propertyCrime;
            Burglary = burglary;
            Theft = theft;
            MotorVehicleTheft = motorVehicleTheft;

        }
    }



    class Program
    {
        static void Main(string[] args)
        {
     
            int year = 0;
            int population = 0;
            int violentCrime = 0;
            int murder = 0;
            int rape = 0;
            int robbery = 0;
            int aggravatedAssault = 0;
            int propertyCrime = 0;
            int burglary = 0;
            int theft = 0;
            int motorVehicleTheft = 0;

            if (args.Length == 2)
            {
                string CrimeCSVFile = args[0];
                string ReportFilePath = args[1];
           
                List<Crime> crimeList = new List<Crime>();
                List<string> Report = new List<string>();

                try
                {
                    using (StreamReader reader = new StreamReader(CrimeCSVFile))
                    //using (var reader = new StreamReader(CrimeCSVFile))
                    {
                        int FileLines = 0;

                        while (!reader.EndOfStream)
                        {
                            var line = reader.ReadLine();
                            var values = line.Split(',');
                            FileLines++;


                            if (FileLines == 1) continue;

                            if (values.Length != 11)
                            {
                                Console.WriteLine("There should be 11 columns");
                                Environment.Exit(0);
                            }
                            
                            try
                            {
                                year = int.Parse(values[0]);
                                population = Int32.Parse(values[1]);
                                violentCrime = Int32.Parse(values[2]);
                                murder = Int32.Parse(values[3]);
                                rape = Int32.Parse(values[4]);
                                robbery = Int32.Parse(values[5]);
                                aggravatedAssault = Int32.Parse(values[6]);
                                propertyCrime = Int32.Parse(values[7]);
                                burglary = Int32.Parse(values[8]);
                                theft = Int32.Parse(values[9]);
                                motorVehicleTheft = Int32.Parse(values[10]);

                                Crime crime = new Crime(year, population, violentCrime, murder, rape,
                                robbery, aggravatedAssault, propertyCrime, burglary, theft, motorVehicleTheft);

                                crimeList.Add(crime);

                                //Testing note... Its printing all the values in the column for each statistic, so that works
                                //Console.WriteLine("crimeList value {0}", crime.Burglary);
                                //Console.WriteLine("year value {0}", crime.Year);

                                //the from Crime I think is one of main sources of the errors
                                var startYear = (from Crime in crimeList select crime.Year).Min();
                                var lastYear = (from Crime in crimeList select crime.Year).Max();
                                var range = lastYear - startYear;

                                Console.WriteLine("Crime Analyzer Report");
                                Console.WriteLine("");
                                Console.WriteLine("Crime Report Period: {0} to {1}", startYear, lastYear);
                                Console.WriteLine("");
                                Console.WriteLine("Total Crime Report Years: {0}", lastYear - startYear);

                                var fifteenThousand = from Crime in crimeList where crime.Murder < 15000 select crime.Year;
                                Console.WriteLine("Years murder is less than 15000: {0}", fifteenThousand);

                                var fiveHThousand = from Crime in crimeList where crime.Robbery > 500000 select crime.Robbery;
                                Console.WriteLine("Years where robberies were greater than 500000: {0}", fiveHThousand);

                                var violentCrimeTwentyTen = from Crime in crimeList where crime.Year == 2010 select (crime.ViolentCrime / crime.Population);
                                Console.WriteLine("Violent crime per capita rate for 2010: {0}", violentCrimeTwentyTen);

                                var averageMurders = (from Crime in crimeList select crime.Murder).Average();
                                Console.WriteLine("Average murder per year (all years): {0}", averageMurders);

                                var avgMNineFourNineSeven = (from Crime in crimeList where crime.Year >= 1994 && crime.Year <= 1997 select crime.Murder).Average();
                                Console.WriteLine("Average number of murders per year for 1994-1997: {0}", avgMNineFourNineSeven);

                                var avgMTwentyTenTwentyThirteen = (from Crime in crimeList where crime.Year >= 2010 && crime.Year <= 2013 select crime.Murder).Average();
                                Console.WriteLine("Average number of murders per year for 2010-2013: {0}", avgMTwentyTenTwentyThirteen);

                                var minTheft = (from Crime in crimeList where crime.Year >= 1999 && crime.Year <= 2004 select crime.Theft).Min();
                                Console.WriteLine("Minimum number of thefts per year for 1999-2004: {0}", minTheft);

                                var maxTheft = (from Crime in crimeList where crime.Year >= 1999 && crime.Year <= 2004 select crime.Theft).Max();
                                Console.WriteLine("Maximum number of thefts per year for 1999-2004: {0}", maxTheft);

                                var highestMVTheft = (from Crime in crimeList where crime.Year >= 1994 && crime.Year <= 2013 select crime.MotorVehicleTheft).Max();
                                Console.WriteLine("Year with highest number of motor vehicle thefts: {0}", highestMVTheft);

                               // Report.Add(range, averageMurders, fifteenThousand, fiveHThousand, violentCrimeTwentyTen, avgMNineFourNineSeven, avgMTwentyTenTwentyThirteen,
                                //    minTheft, maxTheft, highestMVTheft);
                            }


                            catch (Exception e)
                            {
                                Console.WriteLine("Exception: {0}", e);
                            }
                            return;
                        }
                    }

                    //TextWriter tw = new StreamWriter(ReportFilePath);
                    //{
                    //    foreach (String s in Report)
                    //        tw.WriteLine(s);

                    //    tw.Close();
                    //}

                        //Notes on what needs to be answered from the data
                        //What is the range of years included in the data?
                        //How many years of data are included?
                        //What years is the number of murders per year less than 15000 ?
                        //What are the years and associated robberies per year for years where the number of robberies is greater than 500000 ?
                        //What is the violent crime per capita rate for 2010 ? Per capita rate is the number of violent crimes in a year divided by the size of the population that year.
                        //What is the average number of murders per year across all years ?
                        //What is the average number of murders per year for 1994 to 1997 ?
                        //What is the average number of murders per year for 2010 to 2013 ?
                        //What is the minimum number of thefts per year for 1999 to 2004 ?
                        //What is the maximum number of thefts per year for 1999 to 2004 ?
                        //What year had the highest number of motor vehicle thefts ?

                }

                catch (Exception e)
                {
                    Console.WriteLine("The file could not be read:");
                    Console.WriteLine("Exception {0}", e);
                    Environment.Exit(0);
                }
            }

            else if (args.Length != 2)
            {
                Console.WriteLine("Not enough or too many arguments provided");
                Console.WriteLine("CrimeAnalyzer <crime_csv_file_path> <report_file_path>");
                Environment.Exit(0);
            }
        }
    }
}

