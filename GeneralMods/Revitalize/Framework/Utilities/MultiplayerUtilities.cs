using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Revitalize.Framework.Objects;
using StardewValley;

namespace Revitalize.Framework.Utilities
{
    public static class MultiplayerUtilities
    {
        public static string RequestGUIDMessage = "Revitalize.RequestGUIDObject";
        public static string RequestGUIDMessage_Tile = "Revitalize.RequestGUIDObject_Tile";
        public static string ReceieveGUIDMessage = "Revitalize.ReceieveGUIDObject";
        public static string ReceieveGUIDMessage_Tile = "Revitalize.ReceieveGUIDObject_Tile";
        public static void GetModMessage(object o, StardewModdingAPI.Events.ModMessageReceivedEventArgs e)
        {
            ModCore.log("Get a mod message: "+e.Type);
            if (e.Type.Equals(RequestGUIDMessage))
            {
                ModCore.log("Send GUID Request");
                Guid request = Guid.Parse(e.ReadAs<string>());
                SendGuidObject(request);
            }

            if (e.Type.Equals(ReceieveGUIDMessage))
            {
                ModCore.log("Receieve GUID Request");
                string objStr = e.ReadAs <string>();
                var v=ModCore.Serializer.DeserializeFromJSONString<Item>(objStr);
                if (ModCore.CustomObjects.ContainsKey((v as CustomObject).guid) == false)
                {
                    ModCore.CustomObjects.Add((v as CustomObject).guid, (v as CustomObject));
                }
                else
                {
                    ModCore.CustomObjects[(v as CustomObject).guid] = (v as CustomObject);
                }
            }

            if(e.Type.Equals(RequestGUIDMessage_Tile))
            {
                ModCore.log("Send GUID Request FOR TILE");
                Guid request = Guid.Parse(e.ReadAs<string>());
                SendGuidObject_Tile(request);
            }
            if(e.Type.Equals(ReceieveGUIDMessage_Tile))
            {
                ModCore.log("Receieve GUID Request FOR TILE");
                string objStr = e.ReadAs<string>();
                var v = ModCore.Serializer.DeserializeFromJSONString<Item>(objStr);
                if (ModCore.CustomObjects.ContainsKey((v as CustomObject).guid) == false)
                {
                    ModCore.CustomObjects.Add((v as CustomObject).guid, (v as CustomObject));
                }
                else
                {
                    ModCore.CustomObjects[(v as CustomObject).guid] = (v as CustomObject);
                }
            }
        }

        public static void SendGuidObject(Guid request)
        {
            if (ModCore.CustomObjects.ContainsKey(request))
            {
                ModCore.log("Send guid request!");
                ModCore.ModHelper.Multiplayer.SendMessage<string>(ModCore.Serializer.ToJSONString(ModCore.CustomObjects[request]), ReceieveGUIDMessage, new string[] { Revitalize.ModCore.Manifest.UniqueID.ToString() });
            }
            else
            {
                ModCore.log("This mod doesn't have the guid object");
            }
        }

        public static void SendGuidObject_Tile(Guid request)
        {
            if (ModCore.CustomObjects.ContainsKey(request))
            {
                ModCore.log("Send guid tile request!");
                ModCore.ModHelper.Multiplayer.SendMessage<string>(ModCore.Serializer.ToJSONString( (ModCore.CustomObjects[request] as MultiTiledComponent).containerObject), ReceieveGUIDMessage_Tile , new string[] { Revitalize.ModCore.Manifest.UniqueID.ToString() });
            }
            else
            {
                ModCore.log("This mod doesn't have the guid tile");
            }
        }

        public static void RequestGuidObject(Guid request)
        {
            ModCore.ModHelper.Multiplayer.SendMessage<string>(request.ToString(),RequestGUIDMessage, new string[] { ModCore.Manifest.UniqueID.ToString() });
        }

        public static void RequestGuidObject_Tile(Guid request)
        {
            ModCore.ModHelper.Multiplayer.SendMessage<string>(request.ToString(), RequestGUIDMessage_Tile, new string[] { ModCore.Manifest.UniqueID.ToString() });
        }

    }
}
