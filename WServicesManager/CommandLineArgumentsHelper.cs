using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service.Manager
{
    /// <summary>
    /// class that extends CommandLineArguments and holds business logics for this app itself
    /// </summary>
    class CommandLineArgumentsHelper : CommandLineArguments
    {
        private String[] actions = new String[] { 
            "start", "stop", "status", "restart", "debug", "service", 
            "install", "configure", "help", "h", "?" 
        };
        private String[] valids = new String[] { 
            "start", "stop", "status", "restart", "debug", 
            "service", "install", "configure", "config", 
            "help", "h", "?" 
        };

        private String action, configFile;

        public CommandLineArgumentsHelper(String[] args)
            : base(args)
        {
        }

        /// <summary>
        /// check command line arguments against the application specific rules
        /// </summary>
        public void Validate()
        {
            CommandLineArgument[] options = GetOrphanArguments();

            if (options != null && options.Length > 0)
                throw new Exception("Some loose arguments were detected which are not allowed.\r\n\r\n\"" + string.Join("\", \"", options.Select(x => x.Value)) + "\"");

            options = this.GetInvalidArguments(valids);

            if (options != null && options.Length > 0)
                throw new Exception("The following parameters are not valid.\r\n\r\n\"" + string.Join("\", \"", options.Select(x => x.Option)) + "\"");

            // all but configure action requires configuration file
            String[] excepts = new String[] { "configure", "help", "h", "?" };
            if (!excepts.Contains(this.Action) && String.IsNullOrWhiteSpace(this.ConfigurationFile))
                throw new Exception("Configuration file is required for action = \"" + this.Action + "\"");
        }

        public String Action
        {
            get
            {
                if (!String.IsNullOrWhiteSpace(this.action))
                    return this.action;

                CommandLineArgument[] options = GetArguments(actions);

                /* only one action is permitted */
                if (options != null && options.Length > 1)
                    throw new Exception("Multiple actions detected: \"" + string.Join("\", \"", options.Select(x => x.Option)) + "\". There can be only one.");

                /* action takes no complement */
                if (options != null && options.Length > 0 && options[0].Value != null)
                    throw new Exception("Option '" + options[0].Option + "' takes no complement. >>> /" + options[0].Option + " " + options[0].Value + " <<<");

                /* if action isn't specified, defaults it to 'install' or 'service' */
                if (options == null || options.Length == 0)
                    this.action = (Environment.UserInteractive ? "install" : "service");
                else
                    this.action = options[0].Option;

                return this.action;
            }
        }

        public String ConfigurationFile
        {
            get
            {
                if (!String.IsNullOrWhiteSpace(this.configFile))
                    return this.configFile;

                CommandLineArgument option = GetArgument("config");
                this.configFile = option == null ? null : option.Value;

                return this.configFile;
            }
        }
    }
}
