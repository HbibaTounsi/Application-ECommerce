using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_ECommerce.Core.Utility
{
    public static class StaticDetails
    {
        // Statut initial: commande créée, en attente de traitement
        public const string Status_Pending = "Pending";

        // Commande approuvée après paiement, peut être préparée
        public const string Status_Approved = "Approved";

        // Commande prête pour retrait par le client
        public const string Status_ReadyForPickup = "ReadyForPickup";

        // Commande livrée/récupérée, transaction terminée
        public const string Status_Completed = "Completed";

        // Paiement remboursé au client
        public const string Status_Refunded = "Refunded";

        // Commande annulée
        public const string Status_Cancelled = "Cancelled";

    }
}
