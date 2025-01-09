namespace ServerManagement.StateStore
{
    public class TorontoOnlineServersStore : Observer
    {
        private int _numServersOnline;

        public int GetNumberServerOnline()
        {
            return _numServersOnline;
        }

        public void SetNumbersServersOnline(int number)
        {
            _numServersOnline = number;
            base.BroadcastStateChange();
        }
    }
}
