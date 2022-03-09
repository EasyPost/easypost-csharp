using System;
using System.Collections.Generic;
using System.Threading;

namespace EasyPost
{
    public static class ThreadManager
    {
        private static Dictionary<int, string> ThreadApiKeyPairs = new Dictionary<int, string>();

        public static void RegisterThread(int threadId, string apiKey)
        {
            ThreadApiKeyPairs.Add(threadId, apiKey);
        }

        public static void RegisterThread(Thread thread, string apiKey)
        {
            RegisterThread(thread.ManagedThreadId, apiKey);
        }

        internal static string GetApiKeyForThread(int threadId)
        {
            try
            {
                return ThreadApiKeyPairs[threadId];
            }
            catch (KeyNotFoundException)
            {
                throw new Exception("No API key registered for this thread.");
            }
        }

        internal static string GetApiKeyForThread(Thread thread)
        {
            return GetApiKeyForThread(thread.ManagedThreadId);
        }

        internal static string GetApiKeyForCurrentThread()
        {
            return GetApiKeyForThread(Thread.CurrentThread);
        }

        public static void DeregisterThread(int threadId)
        {
            try
            {
                ThreadApiKeyPairs.Remove(threadId);
            }
            catch (KeyNotFoundException)
            {
                // Do nothing
            }
        }

        public static void DeregisterThread(Thread thread)
        {
            DeregisterThread(thread.ManagedThreadId);
        }

        public static void DeregisterAllThreads()
        {
            ThreadApiKeyPairs.Clear();
        }
    }
}
