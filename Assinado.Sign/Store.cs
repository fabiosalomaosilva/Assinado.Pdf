using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;
using Assinado.Pdf.Sign.Model;

namespace Assinado.Pdf.Sign
{
    public class Store
    {
        public List<DigitalCertificate> GetListCertificates(CertSignStoreName storeName, CertSignStoreLocation storeLocation)
        {
            return GetCertificates(storeName, storeLocation);
        }

        public DigitalCertificate[] GetArrayCertificates(CertSignStoreName storeName, CertSignStoreLocation storeLocation)
        {
            var lista = GetCertificates(storeName, storeLocation);
            return lista.ToArray();
        }

        private List<DigitalCertificate> GetCertificates(CertSignStoreName storeName, CertSignStoreLocation storeLocation)
        {
            try
            {
                var store = new X509Store(RetornaStoreName(storeName), RetornaStoreLocation(storeLocation));
                store.Open(OpenFlags.MaxAllowed);
                if (store != null)
                {
                    var collection = store.Certificates;
                    var listaCertificados = collection.Find(X509FindType.FindByKeyUsage, X509KeyUsageFlags.DigitalSignature, true);

                    var list = new List<DigitalCertificate>();
                    foreach (var i in listaCertificados)
                    {
                        if (!string.IsNullOrEmpty(i.FriendlyName))
                        {
                            list.Add(new DigitalCertificate
                            {
                                FriendlyName = i.FriendlyName,
                                Name = i.IssuerName.Name,
                                Certificate = i,
                                PublicKey = i.GetPublicKey()
                            });
                        }
                    }

                    return list;
                }
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }


        public DigitalCertificate GetCertificate(string name)
        {
            var list = GetListCertificates(CertSignStoreName.Root, CertSignStoreLocation.CurrentUser);
            DigitalCertificate cert = null;
            for (var i = 0; i < list.Count; i++)
            {
                var c = list[i];
                if (c.Name == name)
                    cert = c;
            }
            return cert;
        }

        #region  SelectEnum

        private StoreName RetornaStoreName(CertSignStoreName storeName)
        {
            switch (storeName)
            {
                case CertSignStoreName.AddressBook:
                    return StoreName.AddressBook;
                case CertSignStoreName.AuthRoot:
                    return StoreName.AuthRoot;
                case CertSignStoreName.CertificateAuthority:
                    return StoreName.CertificateAuthority;
                case CertSignStoreName.Disallowed:
                    return StoreName.Disallowed;
                case CertSignStoreName.My:
                    return StoreName.My;
                case CertSignStoreName.Root:
                    return StoreName.Root;
                case CertSignStoreName.TrustedPeople:
                    return StoreName.TrustedPeople;
                case CertSignStoreName.TrustedPublisher:
                    return StoreName.TrustedPublisher;
                default:
                    return StoreName.My;
            }
        }
        private StoreLocation RetornaStoreLocation(CertSignStoreLocation storeLocation)
        {
            switch (storeLocation)
            {
                case CertSignStoreLocation.CurrentUser:
                    return StoreLocation.CurrentUser;
                case CertSignStoreLocation.LocalMachine:
                    return StoreLocation.LocalMachine;
                default:
                    return StoreLocation.CurrentUser;
            }
        }
        #endregion


    }
}
