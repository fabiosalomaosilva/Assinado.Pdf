namespace Assinado.Pdf
{
    public enum CertSignStoreLocation
    {
        /// <summary>
        /// The X.509 certificate store used by the current user.
        /// </summary>
        CurrentUser = 1,
        /// <summary>
        /// The X.509 certificate store assigned to the local machine.
        /// </summary>
        LocalMachine = 2,

    }
}
