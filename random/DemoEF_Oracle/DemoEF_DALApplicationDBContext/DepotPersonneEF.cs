﻿using Entite = DemoEF_Entite;
using Microsoft.EntityFrameworkCore;
using DemoEF_DALApplicationDBContext.DTO;

namespace DemoEF_DALApplicationDBContext
{
    public class DepotPersonneEF : Entite.IDepotPersonne
    {
        private ApplicationDBContext m_dbContext;

        public DepotPersonneEF(ApplicationDBContext p_dbContext)
        {
            if (p_dbContext == null)
            {
                throw new ArgumentNullException(nameof(p_dbContext));
            }

            this.m_dbContext = p_dbContext;
        }

        public void AjouterPersonne(Entite.Personne p_personne)
        {
            Personne p = new Personne(p_personne);
            this.m_dbContext.Add(p);
            this.m_dbContext.SaveChanges();
            this.m_dbContext.ChangeTracker.Clear();
        }

        public void MAJPersonne(Entite.Personne p_personne)
        {
            Personne p = new Personne(p_personne);
            this.m_dbContext.Update(p);
            this.m_dbContext.SaveChanges();
            this.m_dbContext.ChangeTracker.Clear();
        }

        public List<Entite.Personne> ObtenirPersonnes(bool inclureAdresse = false)
        {
            IQueryable<Personne> requete = this.m_dbContext.Personnes;
            if (inclureAdresse)
            {
                requete = requete.Include(p => p.AdresseActuelle);
            }
            return requete.Select(p => p.VersEntite()).ToList();
        }
    }
}
