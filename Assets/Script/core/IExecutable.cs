using System;
using System.Collections.Generic;

namespace Script.core
{
    public interface IExecutable
    {
        public void Execute(PlayerBase playerBase, List<NetworkObject> targets = null);
    }
}