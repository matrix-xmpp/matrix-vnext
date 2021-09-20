namespace ConsoleClient
{
    using Matrix;
    using Serilog;
    using System;
    using System.Text;
    using Serilog.Sinks.SystemConsole.Themes;

    public class XmppLoggingHandler : XmppHandler
    {
        public XmppLoggingHandler(XmppClient xmppClient) 
            : base(xmppClient)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console(theme: AnsiConsoleTheme.Code)
                .CreateLogger();

            //xmppClient.Transport.XmlReceived.Subscribe(
            //    xml => { Log.Information($"RECV: {xml}"); }
            //);

            //xmppClient.Transport.XmlSent.Subscribe(
            //    xml => { Log.Information($"SEND: {xml}"); }
            //);

            xmppClient.Transport.DataReceived.Subscribe(
                data => { Log.Information($"RECV: {Encoding.UTF8.GetString(data)}"); }
            );

            xmppClient.Transport.DataSent.Subscribe(
                data => { Log.Information($"SEND: {Encoding.UTF8.GetString(data)}"); }
            );
        }
    }
}
