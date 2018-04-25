
WCF callbacks

Net.tcp dual connection between a web site and a windows app.

Setup:
1. WCF Library dll
2. Web site   (“CallbackNetTcpWebSite”)
3. Windows Forms application. (“_WCF_DesktopClient”)


Dataflow:

When the desktop app starts it sends a message to the web site “ Hello , I am ready to accept messages from you”  
 
The web site creates a txt file in c:\temp\MessageFromWebSite.txt that verifies that a client has connected (change the path if you like)

When a specific page is accessed in the web site –
http://<your local IP>/Testnettcp/send_to_client.aspx -  then a message from the web site is sent to the app when the button is clicked

The app pops  up a message box with the message.

Prerequisites
Installation of net.tcp binding to IIS (Windows 10)

1. Control panel → add Programs and features→ Turn Windows features on or off

2 .Net framework 4.7 Advanced Services → WCF services→ Check Named Piped Activation, TCP activation, TCP Port sharing

3. Go to the IIS Manager.→select the default website and click “Advanced settings” on the right hand side.
The option “Bindings” should have the value “http:*:80:,https:*:443:,net.tcp:808:*,net.pipe:*”. If not maybe restart IIS.

Still on the IIS manager - > Right click on the Default Web Site → Add application. Set the Alias to TestNetTcp and enter the physical path (c:\sourceCode\Web\TestNetTcp\TestNetTcp   on my case).

Select the new application you just created in IIS and click advances setting on the right. Add to the value of the “Enabled Protocols” setting the string ”, net.tcp” (with the coma). The final values should  be:
Enabled protocols   --- http, net.tcp

You need to change the address in the ClientMethods.cs file to your local ip or to localhost.
    string endPointAddressStr = @"net.tcp://10.0.0.70/TestNetTcp/library_service.svc";

You may need to change the config.web file to your local ip or to localhost 
<add baseAddress="net.tcp:/localhost/implementclass" />  

You may need to add the library references again to the "CallbackNetTcpWebSite" and to the "_WCF_DesktopClient" solutions.

MAKE SURE THAT THE Net.Tcp Port Sharing, the Net.Tcp Listener Adapter and the Net.Pipe Adapter (maybe not this one!) ARE RUNNING.



References and Resources

https://sites.google.com/site/wcfpandu/configuring-wcf-service-with-nettcpbinding

https://www.codeproject.com/Articles/29243/A-Windows-Communication-Foundation-WCF-Overview


http://realfiction.net/2008/01/30/The-no-frills-bare-bones-example-to-Duplex-WCF//

https://derekwill.com/2015/05/11/building-duplex-services-with-wcf/





