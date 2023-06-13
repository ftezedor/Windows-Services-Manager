using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Service.Manager
{
    public sealed class CommandLineArgument
    {
        private String option = null;
        private String value = null;

        public CommandLineArgument(String option)
        {
            this.option = option;
        }

        public CommandLineArgument(String option, String value)
        {
            this.option = option;
            this.value = value;
        }

        public String Option
        {
            get { return this.option; }
        }

        public String Value
        {
            get { return this.value; }
            set { this.value = value; }
        }
    }


    public class CommandLineArguments
    {
        protected List<CommandLineArgument> parms = new List<CommandLineArgument>();

        /// <summary>
        /// parse and handle command line parameters
        /// </summary>
        /// <param name="args"></param>
        protected CommandLineArguments(String[] args)
        {
            // to validate the begginig chars of the argument
            Regex rgx1 = new Regex(@"^(-{1,2}|\/)");

            // to validate the whole argument
            Regex rgx2 = new Regex(@"^((-{2}|\/)([\w\-]{2,})|((-|\/)\w{1,2})|(\/\?))$");

            //
            // valid formats are: -w, --word, /w, /word, and /?
            //

            for (int i = 0; i < args.Length; i++)
            {
                if (rgx1.IsMatch(args[i]) && !rgx2.IsMatch(args[i]))
                    throw new ArgumentException("Bad argument name: " + args[i]);

                if (rgx2.IsMatch(args[i]))
                {
                    this.parms.Add(new CommandLineArgument(rgx1.Replace(args[i], String.Empty)));
                }
                else
                {
                    if (this.parms.Last().Value == null)
                        this.parms.Last().Value = args[i];
                    else
                        this.parms.Add(new CommandLineArgument(null, args[i]));
                }
            }
        }

        public CommandLineArgument[] GetArguments(String[] args)
        {
            if (args == null || args.Length == 0)
                return null;

            return this.parms.Where(item => item.Option != null &&
                args.Contains(item.Option)).ToArray<CommandLineArgument>();
        }

        /// <summary>
        /// search for values that have no arguments (loose values)
        /// </summary>
        /// <returns></returns>
        public CommandLineArgument[] GetOrphanArguments()
        {
            return this.parms.Where(item => item.Option == null).ToArray<CommandLineArgument>();
        }

        public CommandLineArgument[] GetInvalidArguments(String[] args)
        {
            if (args == null || args.Length == 0)
                return null;

            return this.parms.Where(item => item.Option != null && !args.Contains(item.Option)).ToArray<CommandLineArgument>();
        }

        /// <summary>
        /// search for entries for the given argument name
        /// </summary>
        /// <remarks>
        /// fails if more than one is found.
        /// for multiple occurrences, try GetParameters
        /// </remarks>
        /// <param name="arg">parameter name</param>
        /// <returns>CommandLineArgument object</returns>
        public CommandLineArgument GetArgument(String arg)
        {
            if (String.IsNullOrWhiteSpace(arg))
                return null;

            CommandLineArgument[] options = this.parms.Where(item => item.Option != null && item.Option.Equals(arg)).ToArray<CommandLineArgument>();

            if (options != null && options.Length > 1)
                throw new Exception("Multiple occurences for the argument '" + arg +
                    "' are not allowed.\r\n\r\n" +
                    String.Join("\r\n", options.Select(e => "/" + e.Option + " " + e.Value).ToArray<String>()));

            return options == null || options.Length == 0 ? null : options[0];
        }
    }
}