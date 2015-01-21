using ChattyData.Models;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Queue;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace ChattyData.Cloud
{
    public class MessageManager
    {
        public MessageManager()
        {
            this.Settings = new SettingsManager();
        }
        public SettingsManager Settings { get; private set; }

        public bool DeleteMessage(Guid messageId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ChattyMessage> GetMessages(int limit = 50, Predicate<ChattyMessage> where = null)
        {
            if (where == null) where = q => true;

            var queue = _getQueue();
            var results = queue.PeekMessages(limit);
            foreach (var item in results)
            {
                //yield return item.ToChattyMessage();
            }
            // this is a hack to make the app compile (while we are defining logic)
            yield break;
        }

        private CloudStorageAccount _getAccount()
        {
            var account = CloudStorageAccount.Parse(this.Settings.StorageConnectionString);
            return account;
        }

        private CloudQueue _getQueue(string queuename = "chitchat")
        {
            var account = _getAccount();
            var queueClient = account.CreateCloudQueueClient();
            var queue = queueClient.GetQueueReference(queuename);
            return queue;
        }

        private CloudBlobContainer _getBlobContainer(string containername = "chatty")
        {
            var account = _getAccount();
            var client = account.CreateCloudBlobClient();
            var container = client.GetContainerReference(containername);
            container.CreateIfNotExists();
            return container;
        }

        public void PostMessage(ChattyMessage content)
        {
            // add item to queue
            var queue = _getQueue();
            queue.AddMessage(new CloudQueueMessage(content.ToString()));

            var container = _getBlobContainer();
            var blob = container.GetBlockBlobReference(content.ToString());

            using (var ms = new MemoryStream())
            {
                var ser = new XmlSerializer(typeof(ChattyMessage));
                ser.Serialize(ms, content);
                ms.Position = 0;
                blob.UploadFromStream(ms);
            }
        }
    }
}