// Decompiled with JetBrains decompiler
// Type: AutoLeadGUI.SSHTest
// Assembly: AutoLeadGUI, Version=2.8.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8777AC84-8195-4D0C-9461-40AEA2B2DD99
// Assembly location: C:\Users\Nguyen Van Dai\Downloads\3.2.1\Debug\AutoLeadGUI.exe

using System;
using System.Collections.Generic;
using System.Threading;

namespace AutoLeadGUI
{
  internal class SSHTest
  {
    private static int __wowId = 0;
    private static object thisLock = new object();
    private static int _threads;
    private static int _currentCount;
    private static Delegate __delegate;
    private static SynchronizedCollection<Thread> threadQueue;
    private static SynchronizedCollection<Thread> runningQueue;
    private static int n;

    public static void stopTestSSHConnections()
    {
      try
      {
        if (SSHTest.threadQueue != null)
          SSHTest.threadQueue.Clear();
        foreach (Thread running in SSHTest.runningQueue)
        {
          if (!running.Equals((object) Thread.CurrentThread) && running.IsAlive)
            running.Abort();
        }
        SSHTest.runningQueue.Clear();
      }
      catch
      {
      }
    }

    public static void stopTestSSHConnectionsNoAbort()
    {
      if (SSHTest.threadQueue == null)
        return;
      SSHTest.threadQueue.Clear();
    }

    public static int doTestSSHConnections(Array sshs, Delegate _delegate, int threads)
    {
      ++SSHTest.__wowId;
      SSHTest.n = 0;
      if (SSHTest.threadQueue != null)
        SSHTest.threadQueue.Clear();
      SSHTest.runningQueue = new SynchronizedCollection<Thread>();
      SSHTest._threads = threads > 0 ? threads : 1;
      SSHTest.__delegate = _delegate;
      SSHTest._currentCount = 0;
      for (int index = 0; index < sshs.Length; ++index)
      {
        Dictionary<string, object> ssh = (Dictionary<string, object>) sshs.GetValue(index);
        if (!ssh.ContainsKey("status"))
        {
          if (SSHTest._currentCount < SSHTest._threads)
          {
            ++SSHTest._currentCount;
            int j = index;
            int k = SSHTest.__wowId;
            Thread thread = new Thread((ThreadStart) (() => SSHTest.___method(ssh, j, k)));
            thread.Start();
            SSHTest.runningQueue.Add(thread);
          }
          else if (SSHTest.threadQueue == null)
          {
            int j = index;
            int k = SSHTest.__wowId;
            SSHTest.threadQueue = new SynchronizedCollection<Thread>();
            Thread thread = new Thread((ThreadStart) (() => SSHTest.___method(ssh, j, k)));
            SSHTest.threadQueue.Add(thread);
          }
          else
          {
            int j = index;
            int k = SSHTest.__wowId;
            Thread thread = new Thread((ThreadStart) (() => SSHTest.___method(ssh, j, k)));
            SSHTest.threadQueue.Add(thread);
          }
          ++SSHTest.n;
        }
      }
      return SSHTest.n;
    }

    private static void ___method(Dictionary<string, object> ssh, int i, int wowId)
    {
      try
      {
        if (wowId != SSHTest.__wowId)
          return;
        Console.WriteLine("*** start [" + (object) i + "]");
        string _host = ssh["host"].ToString();
        int int32 = Convert.ToInt32(ssh["port"].ToString());
        string _username = ssh["username"].ToString();
        string _password = ssh["password"].ToString();
        string str = (string) null;
        TimeoutSSHClient timeoutSshClient = new TimeoutSSHClient(50, _host, _username, _password, int32);
        bool flag;
        if (timeoutSshClient.isLive())
        {
          flag = true;
          str = timeoutSshClient.fingerPrint;
        }
        else
          flag = false;
        if (wowId != SSHTest.__wowId)
          return;
        object[] objArray = new object[4]
        {
          (object) ssh,
          (object) i,
          (object) flag,
          (object) str
        };
        lock (SSHTest.thisLock)
        {
          if (SSHTest.threadQueue != null && SSHTest.threadQueue.Count > 0)
          {
            SSHTest.__delegate.DynamicInvoke(objArray);
            Thread thread = SSHTest.threadQueue[0];
            SSHTest.threadQueue.RemoveAt(0);
            thread.Start();
            SSHTest.runningQueue.Add(thread);
            SSHTest.runningQueue.Remove(Thread.CurrentThread);
          }
          else
          {
            SSHTest.__delegate.DynamicInvoke(objArray);
            --SSHTest._currentCount;
            SSHTest.runningQueue.Remove(Thread.CurrentThread);
          }
        }
      }
      catch
      {
        Console.WriteLine("***");
      }
    }
  }
}
