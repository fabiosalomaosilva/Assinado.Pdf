namespace Assinado.Pdf
{
    public enum CertSignStoreName
    {
        /// <summary>
        /// The X.509 certificate store for other users.
        /// </summary>
        AddressBook = 1,
        /// <summary>
        /// The X.509 certificate store for third-party certificate authorities (CAs).
        /// </summary>
        AuthRoot = 2,
        /// <summary>
        /// The X.509 certificate store for intermediate certificate authorities (CAs).
        /// </summary>
        CertificateAuthority = 3,
        /// <summary>
        /// The X.509 certificate store for revoked certificates.
        /// </summary>
        Disallowed = 4,
        /// <summary>
        /// The X.509 certificate store for personal certificates.
        /// </summary>
        My = 5,
        /// <summary>
        /// The X.509 certificate store for trusted root certificate authorities (CAs).
        /// </summary>
        Root = 6,
        /// <summary>
        /// The X.509 certificate store for directly trusted people and resources.
        /// </summary>
        TrustedPeople = 7,
        /// <summary>
        /// The X.509 certificate store for directly trusted publishers.
        /// </summary>
        TrustedPublisher = 8,
    }
}
