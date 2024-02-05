using System.Collections.Generic;

namespace Script.core
{
    public interface IExecutable
    {
        public void Execute(Character character, List<NetworkObject> targets = null);
    }
}