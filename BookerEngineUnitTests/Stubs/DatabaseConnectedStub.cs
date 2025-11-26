using System;
using System.Collections.Generic;
using System.Text;

namespace BookerEngineUnitTests.Stubs
{
    internal class DatabaseConnectedStub : DatabaseNotImplementedStub
    {
        public override bool IsConnected()
        {
            return true;
        }
    }
}
