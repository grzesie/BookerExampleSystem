using System;
using System.Collections.Generic;
using System.Text;

namespace BookerEngineUnitTests.Stubs
{
    internal class DatabaseNotConnectedStub : DatabaseNotImplementedStub
    {
        public override bool IsConnected()
        {
            return false;
        }
    }
}
