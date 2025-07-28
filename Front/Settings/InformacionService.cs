namespace Front.Settings
{
    public class InformacionService
    {
        public event Action? InformacionActualizada;

        public void ActualizarInformacion()
        {
            InformacionActualizada?.Invoke();
        }
    }
}