using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using _Wcf_Library_dll;

public partial class send_to_client : System.Web.UI.Page
{
  protected void Page_Load(object sender, EventArgs e)
  {

  }

  protected void Button1_Click(object sender, EventArgs e)
  {
    List<IClientMethods> channelList = (List<IClientMethods>)HttpRuntime.Cache["callbackChannelsList"];


    if (channelList != null && channelList.Count > 0)
    {


      // use callbacks to send the buffer to the client app.


      for (int i = channelList.Count - 1; i >= 0; i--)
      {

        try
        {
          channelList[i].CallBackFunction(MessageBox.Text);
        }
        catch (CommunicationObjectAbortedException)
        {
          string msg = "Client Aborted Channel: " + channelList[i].GetHashCode();
          Response.Write(msg);
          channelList.RemoveAt(i);
          // Update Cache
          HttpRuntime.Cache.Insert("callbackChannelsList",
               channelList,
               null,
               System.Web.Caching.Cache.NoAbsoluteExpiration,
               System.Web.Caching.Cache.NoSlidingExpiration,
               System.Web.Caching.CacheItemPriority.Default,
               null);
        }

        catch (CommunicationObjectFaultedException)
        {
          Response.Write("Client Died in Channel: " + channelList[i].GetHashCode());
          channelList.RemoveAt(i);

          HttpRuntime.Cache.Insert("callbackChannelsList",
               channelList,
               null,
               System.Web.Caching.Cache.NoAbsoluteExpiration,
               System.Web.Caching.Cache.NoSlidingExpiration,
               System.Web.Caching.CacheItemPriority.Default,
               null);


        }
        catch (Exception)
        {
          Response.Write("Unknown Error in Channel: " + channelList[i].GetHashCode());
          channelList.RemoveAt(i);

          HttpRuntime.Cache.Insert("callbackChannelsList",
               channelList,
               null,
               System.Web.Caching.Cache.NoAbsoluteExpiration,
               System.Web.Caching.Cache.NoSlidingExpiration,
               System.Web.Caching.CacheItemPriority.Default,
               null);


        }
      }

    }

  }
}