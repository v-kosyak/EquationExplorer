using System;
using System.Diagnostics;
using System.Linq;
using EquationExplorer;

namespace EquationsConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var factory = new DoubleEquationFactory();
                
                var parser = factory.CreateParser();

                var members = args.Select(arg => ParseArgument(arg, parser));

                var minValue = ReadParameter("Input minimal value for equation", parser, 381);
                var maxValue = ReadParameter("Input maximum value for equation", parser, 381);
                var allVariations = ReadParameter("Input if try all subsets of memebers", Boolean.Parse, false);

                var resolver = SetupResolver(factory);
                resolver.AllVariations = allVariations;
                resolver.Filter = eq => eq.Value >= minValue && eq.Value <= maxValue;


                var timeStamp = new Stopwatch();
                timeStamp.Start();

                var equations = resolver.Resolve(members.ToArray());
                
                equations.OrderBy(eq => eq.Value).ToList().ForEach(
                        equation => Console.WriteLine("{0} = {1}", equation.ToString(), equation.Value));

                Console.WriteLine("{0} equations found", equations.Count());

                timeStamp.Stop();
                Console.WriteLine("Execution time (ms): {0}", timeStamp.ElapsedMilliseconds);
            }
            catch(Exception e)
            {
                Console.WriteLine("Equation resolutution failed:\n{0}", e.Message);
            }

            Console.ReadKey();
        }

        private static T ParseArgument<T>(string arg, Func<string, T> parser)
        {
            try
            {
                return parser(arg);
            }
            catch (Exception e)
            {
                throw new InvalidCastException(String.Format("Cannot parse {0} as value of type {1}", arg, typeof(T)), e);
            }
        }

        private static T ReadParameter<T>(string prompt, Func<string, T> parser, T defaultValue)
        {
            Console.Write("{0} ({1}):", prompt, defaultValue);

            var parameter = Console.ReadLine();

            if (String.IsNullOrEmpty(parameter))
                return defaultValue;

            return parser(parameter);
        }

        private static EquationResolver<T> SetupResolver<T>(IEquationFactory<T> equationFactory)
        {
            var resolver = equationFactory.CreateResolver();

            equationFactory.AddAdditionOperator(resolver);
            equationFactory.AddSubstructionOperator(resolver);
            equationFactory.AddMultiplicationOperator(resolver);
            equationFactory.AddDivisionOperator(resolver);

            return resolver;
        }

    }
}
