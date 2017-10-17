namespace kvaselector.Migrations
{
    using kvaselector.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<kvaselector.Models.kvaselectorContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(kvaselector.Models.kvaselectorContext context)
        {
            context.FavoriteNames.AddOrUpdate(f => f.Id, new FavoriteName
            {
                Id = 1,
                Name = "Favorit 1"
            });

            context.FavoriteNames.AddOrUpdate(f => f.Id, new FavoriteName
            {
                Id = 2,
                Name = "Favorit 2"
            });

            context.FavoriteNames.AddOrUpdate(f => f.Id, new FavoriteName
            {
                Id = 3,
                Name = "Senast anv�nda koder"
            });

            context.FavoriteNames.AddOrUpdate(f => f.Id, new FavoriteName
            {
                Id = 4,
                Name = "Inga f�rvalda koder"
            });

            context.FavoriteNames.AddOrUpdate(f => f.Id, new FavoriteName
            {
                Id = 5,
                Name = "Alla kryssrutor"
            });

            //context.FavoriteServices.AddOrUpdate(f => f.Id, new FavoriteService
            //{
            //    Id = 1,
            //    ServiceTypeId = 1,
            //    FavoriteNameId = 1
            //});

            //context.FavoriteServices.AddOrUpdate(f => f.Id, new FavoriteService
            //{
            //    Id = 2,
            //    ServiceTypeId = 1,
            //    FavoriteNameId = 2
            //});

            //context.FavoriteServices.AddOrUpdate(f => f.Id, new FavoriteService
            //{
            //    Id = 3,
            //    ServiceTypeId = 1,
            //    FavoriteNameId = 3
            //});

            context.FavoriteServices.AddOrUpdate(f => f.Id, new FavoriteService
            {
                Id = 1,
                ServiceTypeId = 1,
                FavoriteNameId = 4
            });

            context.FavoriteServices.AddOrUpdate(f => f.Id, new FavoriteService
            {
                Id = 2,
                ServiceTypeId = 1,
                FavoriteNameId = 5
            });

            context.FavoriteServices.AddOrUpdate(f => f.Id, new FavoriteService
            {
                Id = 3,
                ServiceTypeId = 2,
                FavoriteNameId = 4
            });

            context.FavoriteServices.AddOrUpdate(f => f.Id, new FavoriteService
            {
                Id = 4,
                ServiceTypeId = 2,
                FavoriteNameId = 5
            });

            context.FavoriteServices.AddOrUpdate(f => f.Id, new FavoriteService
            {
                Id = 5,
                ServiceTypeId = 3,
                FavoriteNameId = 4
            });

            context.FavoriteServices.AddOrUpdate(f => f.Id, new FavoriteService
            {
                Id = 6,
                ServiceTypeId = 3,
                FavoriteNameId = 5
            });

            context.ServiceTypes.AddOrUpdate(s => s.Id, new ServiceType
            {
                Id = 1,
                Name = "Bes�k p� mottagning",
                Description = "�ppenv�rdsbes�k p� mottagning d�r en patient m�ter en h�lso- och sjukv�rdspersonal med sj�lvst�ndigt" +
                " behandlingsansvar." + "\n" +
                "Nybes�k: �ppenv�rdsbes�k som inte har medicinskt samband med tidigare bes�k eller v�rdtillf�lle inom samma medicinska" +
                " verksamhetsomr�de(klinik / basenhet / motsvarande), v�rdcentral eller motsvarande." + "\n" +
                "�terbes�k: �ppenv�rdsbes�k som har medicinskt samband med tidigare bes�k eller v�rdtillf�lle inom samma medicinska" +
                " verksamhetsomr�de(klinik / basenhet / motsvarande), v�rdcentral eller motsvarande."
            });

            context.ServiceTypes.AddOrUpdate(s => s.Id, new ServiceType
            {
                Id = 2,
                Name = "Resurskr�vande bes�k barn och ungdom under 18 �r",
                Description = "�ppenv�rdsbes�k p� mottagning d�r en patient m�ter en h�lso- och sjukv�rdspersonal med sj�lvst�ndigt" +
               " behandlingsansvar." + "\n" +
               "Nybes�k: �ppenv�rdsbes�k som inte har medicinskt samband med tidigare bes�k eller v�rdtillf�lle inom samma medicinska" +
               " verksamhetsomr�de(klinik / basenhet / motsvarande), v�rdcentral eller motsvarande." + "\n" +
               "�terbes�k: �ppenv�rdsbes�k som har medicinskt samband med tidigare bes�k eller v�rdtillf�lle inom samma medicinska" +
               " verksamhetsomr�de(klinik / basenhet / motsvarande), v�rdcentral eller motsvarande."
            });

            context.ServiceTypes.AddOrUpdate(s => s.Id, new ServiceType
            {
                Id = 3,
                Name = "Gruppbes�k",
                Description = "�ppenv�rdsbes�k p� mottagning d�r fler patienter i grupp m�ter en h�lso- och sjukv�rdspersonal." +
                "\n" +
                "En grupp ska best� av minst 2 och h�gst 6 patienter."
            });

            context.ServiceTypes.AddOrUpdate(s => s.Id, new ServiceType
            {
                Id = 4,
                Name = "Hembes�k",
                Description = "�ppenv�rdsbes�k i patientens bostad eller motsvarande d�r patienten m�ter en h�lso- och " +
                "sjukv�rdspersonal med sj�lvst�ndigt behandlingsansvar."
            });

            context.ServiceTypes.AddOrUpdate(s => s.Id, new ServiceType
            {
                Id = 5,
                Name = "Omfattande utredning barn och ungdom under 18 �r, tal och spr�k",
                Description = "I utredning ing�r omfattande testning, analys, kvalificerade utl�tanden och �verf�ring av " +
                "resultat till patient/n�rst�ende. �verf�ring av resultat till patient och eller n�rst�ende ing�r i utredningen " +
                "och rapporteras vid bes�k med KV� f�r avslutad utredning. Se �Riktlinjer f�r V�rdval Logopedi� p� V�rdgivarguiden." +
                "\n" +
                "En utredning som genomf�rs under en och samma dag inkl. �verf�ring av resultat till patient och eller n�rst�ende, " +
                "rapporteras p� samma bes�k med KV� f�r p�b�rjad och avslutad utredning."
            });

            context.TreatmentTypes.AddOrUpdate(p => p.Id, new TreatmentType
            {
                Id = 1,
                Name = "Liten lista: Diagnostisk �tg�rd"
            });

            context.TreatmentTypes.AddOrUpdate(p => p.Id, new TreatmentType
            {
                Id = 2,
                Name = "Liten lista: Behandlande �tg�rd"
            });

            context.TreatmentTypes.AddOrUpdate(p => p.Id, new TreatmentType
            {
                Id = 3,
                Name = "Liten lista: Utan patientbes�k"
            });

            context.TreatmentTypes.AddOrUpdate(p => p.Id, new TreatmentType
            {
                Id = 4,
                Name = "Liten lista: Till�ggsuppgift"
            });

            context.TreatmentTypes.AddOrUpdate(p => p.Id, new TreatmentType
            {
                Id = 5,
                Name = "Rapportera tid med en av koderna i denna lista."
            });

            context.TreatmentTypes.AddOrUpdate(p => p.Id, new TreatmentType
            {
                Id = 6,
                Name = "Stor lista"
            });

            context.TreatmentTypes.AddOrUpdate(p => p.Id, new TreatmentType
            {
                Id = 7,
                Name = "Vid �terbes�k uppm�rksammas framf�r allt nedan angivna �tg�rder. De kan antingen v�ljas fr�n listorna ovan eller fr�n kryssrutorna nedan."
            });

            context.TreatmentTypes.AddOrUpdate(p => p.Id, new TreatmentType
            {
                Id = 8,
                Name = "Rapporteras d� f�retr�dare representerar patienten. �tg�rden kan antingen v�ljas fr�n listorna ovan eller fr�n kryssrutan nedan."
            });

            context.TreatmentTypes.AddOrUpdate(p => p.Id, new TreatmentType
            {
                Id = 9,
                Name = "Vid �terbes�k uppm�rksammas framf�r allt nedan angivna �tg�rder. De kan antingen v�ljas fr�n listorna ovan eller fr�n kryssrutorna nedan."
            });

            context.TreatmentTypes.AddOrUpdate(p => p.Id, new TreatmentType
            {
                Id = 10,
                Name = "Ange KV�-kod UX001 Nybes�k i f�rekommande fall."
            });

            context.TreatmentTypes.AddOrUpdate(p => p.Id, new TreatmentType
            {
                Id = 11,
                Name = "Vid varje enskilt bes�k ska minst en av nedanst�ende KV�-koder f�r utredning rapporteras. Avslutad utredning kr�ver " +
                        "tidigare rapporterade bes�k d�r det framg�r att utredningen har p�b�rjats och vid behov p�g�r."
            });

            context.TreatmentTypes.AddOrUpdate(p => p.Id, new TreatmentType
            {
                Id = 12,
                Name = "D�rut�ver ska samtliga nedan angivna KV�-koder vid n�got tillf�lle ha rapporterats under utredningen."
            });

            context.TreatmentTypes.AddOrUpdate(p => p.Id, new TreatmentType
            {
                Id = 13,
                Name = "Nedan angivna KV�-kod rapporteras d� bes�ket �ven avser �verf�ring till skola i samband med �verf�ring till n�rst�ende."
            });

            context.Diagnosis.AddOrUpdate(p => p.Id, new Diagnosis
            {
                Id = 1,
                Code = "F80.0A, Fonologisk spr�kst�rning",
                Description = "Fonologisk spr�kst�rning"
            });

            context.Diagnosis.AddOrUpdate(p => p.Id, new Diagnosis
            {
                Id = 2,
                Code = "F80.0B, Utvecklingsf�rsening av oralmotorik",
                Description = "Utvecklingsf�rsening av oralmotorik"
            });

            context.Diagnosis.AddOrUpdate(p => p.Id, new Diagnosis
            {
                Id = 3,
                Code = "F80.0C, Dyslali och liknande artikulationsavvikelse",
                Description = "Dyslali och liknande artikulationsavvikelse"
            });

            context.Diagnosis.AddOrUpdate(p => p.Id, new Diagnosis
            {
                Id = 4,
                Code = "F80.1A, Grammatisk spr�kst�rning",
                Description = "Grammatisk spr�kst�rning"
            });

            context.Diagnosis.AddOrUpdate(p => p.Id, new Diagnosis
            {
                Id = 5,
                Code = "F80.1B, Fonologisk och grammatisk spr�kst�rning",
                Description = "Fonologisk och grammatisk spr�kst�rning"
            });

            context.Diagnosis.AddOrUpdate(p => p.Id, new Diagnosis
            {
                Id = 6,
                Code = "F80.1C, Semantisk spr�kst�rning",
                Description = "Semantisk spr�kst�rning"
            });

            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 1,
                TreatmentTypeId = 1,
                Code = "AV078, Inspelning av r�st, tal och spr�k",
                Name = "Inspelning av r�st, tal och spr�k",
                Description = "Dokumentation av textl�sning, spontantal, uth�llna vokaler m.m"
            });

            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 2,
                TreatmentTypeId = 1,
                Code = "AV093, Perceptuell lyssnarbed�mning av r�st och tal",
                Name = "Perceptuell lyssnarbed�mning av r�st och tal",
                Description = "saknas"
            });

            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 3,
                TreatmentTypeId = 1,
                Code = "AV048, Videodokumentation UNS",
                Name = "Videodokumentation UNS",
                Description = "Dokumentation med r�rliga bilder och eventuellt �ven med ljud"
            });

            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 4,
                TreatmentTypeId = 1,
                Code = "AV045, Unders�kning av tal",
                Name = "Unders�kning av tal",
                Description = "Innefattar unders�kning av ett eller flera av f�ljande omr�den: andning, fonation, oralmotorik, talflyt, artikulation, prosodi och f�rst�elighet"
            });

            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 5,
                TreatmentTypeId = 1,
                Code = "AV108, Unders�kning av spr�k",
                Name = "Unders�kning av spr�k",
                Description = "Innefattar unders�kning av ett eller flera av f�ljande omr�den: spontantal, icke-verbal kommunikation, pragmatik, spr�kf�rst�else, auditiv f�rst�else, symbolf�rst�else, minne, fonologi, grammatik, lexikon, syntax, semantik, ben�mningsf�rm�ga, repetitionsf�rm�ga, spr�klig medvetenhet samt l�s- och skrivf�rm�ga"
            });

            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 6,
                TreatmentTypeId = 2,
                Code = "DV089, Logopedisk behandling",
                Name = "Logopedisk behandling",
                Description = "saknas"
            });

            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 7,
                TreatmentTypeId = 2,
                Code = "GA024, Utprovning och f�rskrivning av utrustning som ej ing�r i hj�lpmedelsf�rteckningen",
                Name = "Utprovning och f�rskrivning av utrustning som ej ing�r i hj�lpmedelsf�rteckningen",
                Description = "T ex AKK (Alternativ och Kompletterande Kommunikation) eller oralmotoriska hj�lpmedel, inkl gomplatta."
            });

            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 8,
                TreatmentTypeId = 2,
                Code = "GA025, Utprovning och f�rskrivning av �vriga hj�lpmedel (exklusive rullstol)",
                Name = "Utprovning och f�rskrivning av �vriga hj�lpmedel (exklusive rullstol)",
                Description = "Information, utprovning, f�rskrivning, specialanpassning, tr�ning och uppf�ljning av hj�lpmedel som ing�r i hj�lpmedelsf�rteckningen, (exklusive rullstol)"
            });

            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 9,
                TreatmentTypeId = 2,
                Code = "DD003, Byte av r�stventil",
                Name = "Byte av r�stventil",
                Description = "Byte av r�stprotes/r�stventil utan operativ �tg�rd"
            });

            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 10,
                TreatmentTypeId = 2,
                Code = "GB002, Information och undervisning",
                Name = "Information och undervisning",
                Description = "Information och utbildning av v�sentligt st�rre omfattning �n vad som f�rekommer vid ordin�ra kontakter, t ex skolor av olika slag, omfattande tr�ningsmaterial etc"
            });

            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 11,
                TreatmentTypeId = 3,
                Code = "XS003, Brevkontakt med patient",
                Name = "Brevkontakt med patient",
                Description = "Innefattar skriftlig kommunikation med patient. Brev, e-mail m. m"
            });

            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 12,
                TreatmentTypeId = 3,
                Code = "XS011, Telefonkontakt med anh�riga",
                Name = "Telefonkontakt med anh�riga",
                Description = "Samtalet skall journalf�ras"
            });

            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 13,
                TreatmentTypeId = 3,
                Code = "XS012, Telefonkontakt med patient",
                Name = "Telefonkontakt med patient",
                Description = "Telefon med patient. Samtalet skall journalf�ras"
            });

            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 14,
                TreatmentTypeId = 3,
                Code = "GD002, Intyg, enklare",
                Name = "Intyg, enklare",
                Description = "Till exempel sjukintyg, friskintyg, k�rkortsintyg, handikappers�ttning, intyg till skolor etc"
            });

            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 15,
                TreatmentTypeId = 3,
                Code = "GD003, Intyg, omfattande",
                Name = "Intyg, omfattande",
                Description = "T.ex. LUH, r�ttsintyg, intyg f�r bostadsanpassning eller annat intyg av motsvarande omfattning"
            });

            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 16,
                TreatmentTypeId = 4,
                Code = "ZV020, Anv�ndande av tolk",
                Name = "Anv�ndande av tolk",
                Description = "saknas"
            });

            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 17,
                TreatmentTypeId = 4,
                Code = "ZV021, Arbetsplatsbes�k",
                Name = "Arbetsplatsbes�k",
                Description = "saknas"
            });

            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 18,
                TreatmentTypeId = 4,
                Code = "ZV023, Bes�k inom barnomsorgen och i skolan",
                Name = "Bes�k inom barnomsorgen och i skolan",
                Description = "Bes�k i f�rskola/skola och hos dagmamma"
            });

            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 19,
                TreatmentTypeId = 4,
                Code = "ZV025, Hembes�k",
                Name = "Hembes�k",
                Description = "Bes�k i patients bostad eller d�r patient vistas"
            });

            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 20,
                TreatmentTypeId = 4,
                Code = "ZV043, �vriga bes�k utanf�r v�rdenheten",
                Name = "�vriga bes�k utanf�r v�rdenheten",
                Description = "saknas"
            });

            //Liten lista, TreatmentTypeId = 5, V�rdtj�nst(er): Resurskr�vande bes�k barn och ungdom under 18 �r
            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 21,
                TreatmentTypeId = 5,
                Code = "ZV211, 060-119 minuter (i)",
                Name = "060-119 minuter (i)",
                Description = "Till�ggskod f�r angivelse av tids�tg�ng"
            });

            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 22,
                TreatmentTypeId = 5,
                Code = "ZV212, 120-179 minuter (i)",
                Name = "120-179 minuter (i)",
                Description = "Till�ggskod f�r angivelse av tids�tg�ng"
            });

            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 23,
                TreatmentTypeId = 5,
                Code = "ZV213, 180-239 minuter (i)",
                Name = "180-239 minuter (i)",
                Description = "Till�ggskod f�r angivelse av tids�tg�ng"
            });

            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 24,
                TreatmentTypeId = 5,
                Code = "ZV214, 240-299 minuter (i)",
                Name = "240-299 minuter (i)",
                Description = "Till�ggskod f�r angivelse av tids�tg�ng"
            });

            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 25,
                TreatmentTypeId = 5,
                Code = "ZV215, 300-359 minuter (i)",
                Name = "300-359 minuter (i)",
                Description = "Till�ggskod f�r angivelse av tids�tg�ng"
            });

            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 26,
                TreatmentTypeId = 5,
                Code = "ZV216, 360-419 minuter (i)",
                Name = "360-419 minuter (i)",
                Description = "Till�ggskod f�r angivelse av tids�tg�ng"
            });

            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 27,
                TreatmentTypeId = 5,
                Code = "ZV217, 420-479 minuter (i)",
                Name = "420-479 minuter (i)",
                Description = "Till�ggskod f�r angivelse av tids�tg�ng"
            });

            //Stor lista, TreatmentTypeId = 6
            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 28,
                TreatmentTypeId = 6,
                Code = "AV078, Inspelning av r�st, tal och spr�k",
                Name = "Inspelning av r�st, tal och spr�k",
                Description = "Dokumentation av textl�sning, spontantal, uth�llna vokaler m.m"
            });

            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 29,
                TreatmentTypeId = 6,
                Code = "AV093, Perceptuell lyssnarbed�mning av r�st och tal",
                Name = "Perceptuell lyssnarbed�mning av r�st och tal",
                Description = "saknas"
            });

            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 30,
                TreatmentTypeId = 6,
                Code = "AV048, Videodokumentation UNS",
                Name = "Videodokumentation UNS",
                Description = "Dokumentation med r�rliga bilder och eventuellt �ven med ljud"
            });

            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 31,
                TreatmentTypeId = 6,
                Code = "AV045, Unders�kning av tal",
                Name = "Unders�kning av tal",
                Description = "Innefattar unders�kning av ett eller flera av f�ljande omr�den: andning, fonation, oralmotorik, talflyt, artikulation, prosodi och f�rst�elighet"
            });

            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 32,
                TreatmentTypeId = 6,
                Code = "AV108, Unders�kning av spr�k",
                Name = "Unders�kning av spr�k",
                Description = "Innefattar unders�kning av ett eller flera av f�ljande omr�den: spontantal, icke-verbal kommunikation, pragmatik, spr�kf�rst�else, auditiv f�rst�else, symbolf�rst�else, minne, fonologi, grammatik, lexikon, syntax, semantik, ben�mningsf�rm�ga, repetitionsf�rm�ga, spr�klig medvetenhet samt l�s- och skrivf�rm�ga"
            });

            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 33,
                TreatmentTypeId = 6,
                Code = "DV089, Logopedisk behandling",
                Name = "Logopedisk behandling",
                Description = "saknas"
            });

            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 34,
                TreatmentTypeId = 6,
                Code = "GA024, Utprovning och f�rskrivning av utrustning som ej ing�r i hj�lpmedelsf�rteckningen",
                Name = "Utprovning och f�rskrivning av utrustning som ej ing�r i hj�lpmedelsf�rteckningen",
                Description = "T ex AKK (Alternativ och Kompletterande Kommunikation) eller oralmotoriska hj�lpmedel, inkl gomplatta"
            });

            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 35,
                TreatmentTypeId = 6,
                Code = "GA025, Utprovning och f�rskrivning av �vriga hj�lpmedel (exklusive rullstol)",
                Name = "Utprovning och f�rskrivning av �vriga hj�lpmedel (exklusive rullstol)",
                Description = "Information, utprovning, f�rskrivning, specialanpassning, tr�ning och uppf�ljning av hj�lpmedel som ing�r i hj�lpmedelsf�rteckningen, (exklusive rullstol)"
            });

            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 36,
                TreatmentTypeId = 6,
                Code = "DD003, Byte av r�stventil",
                Name = "Byte av r�stventil",
                Description = "Byte av r�stprotes/r�stventil utan operativ �tg�rd"
            });

            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 37,
                TreatmentTypeId = 6,
                Code = "GB002, Information och undervisning",
                Name = "Information och undervisning",
                Description = "Information och utbildning av v�sentligt st�rre omfattning �n vad som f�rekommer vid ordin�ra kontakter, t ex skolor av olika slag, omfattande tr�ningsmaterial etc"
            });

            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 38,
                TreatmentTypeId = 6,
                Code = "XS003, Brevkontakt med patient",
                Name = "Brevkontakt med patient",
                Description = "Innefattar skriftlig kommunikation med patient. Brev, e-mail m. m"
            });

            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 39,
                TreatmentTypeId = 6,
                Code = "XS011, Telefonkontakt med anh�riga",
                Name = "Telefonkontakt med anh�riga",
                Description = "Samtalet skall journalf�ras"
            });

            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 40,
                TreatmentTypeId = 6,
                Code = "XS012, Telefonkontakt med patient",
                Name = "Telefonkontakt med patient",
                Description = "Telefon med patient. Samtalet skall journalf�ras"
            });

            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 41,
                TreatmentTypeId = 6,
                Code = "GD002, Intyg, enklare",
                Name = "Intyg, enklare",
                Description = "Till exempel sjukintyg, friskintyg, k�rkortsintyg, handikappers�ttning, intyg till skolor etc"
            });

            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 42,
                TreatmentTypeId = 6,
                Code = "GD003, Intyg, omfattande",
                Name = "Intyg, omfattande",
                Description = "T.ex. LUH, r�ttsintyg, intyg f�r bostadsanpassning eller annat intyg av motsvarande omfattning"
            });

            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 43,
                TreatmentTypeId = 6,
                Code = "ZV020, Anv�ndande av tolk",
                Name = "Anv�ndande av tolk",
                Description = "saknas"
            });

            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 44,
                TreatmentTypeId = 6,
                Code = "ZV021, Arbetsplatsbes�k",
                Name = "Arbetsplatsbes�k",
                Description = "saknas"
            });

            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 45,
                TreatmentTypeId = 6,
                Code = "ZV023, Bes�k inom barnomsorgen och i skolan",
                Name = "Bes�k inom barnomsorgen och i skolan",
                Description = "Bes�k i f�rskola/skola och hos dagmamma"
            });

            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 46,
                TreatmentTypeId = 6,
                Code = "ZV025, Hembes�k",
                Name = "Hembes�k",
                Description = "Bes�k i patients bostad eller d�r patient vistas"
            });

            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 47,
                TreatmentTypeId = 6,
                Code = "ZV043, �vriga bes�k utanf�r v�rdenheten",
                Name = "�vriga bes�k utanf�r v�rdenheten",
                Description = "saknas"
            });

            //Krysslista, TreatmentTypeId = 7. V�rdtj�nst(er): Bes�k p� mottagning
            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 48,
                TreatmentTypeId = 7,
                Code = "AU120, Uppr�ttande av strukturerad v�rd- och omsorgsplan (i)",
                Name = "Uppr�ttande av strukturerad v�rd- och omsorgsplan (i)",
                Description = "Uppr�ttande/reviderande av skriftlig strukturerad v�rd- och omsorgsplan Planen ska om m�jligt utformas " +
                                "tillsammans med ber�rd individ. I planen ska beskrivas planerad och beslutad v�rd och omsorg. F�r " +
                                "�tg�rderna i planen ska anges m�l. Planen ska utv�rderas och ompr�vas. Den ska dokumenteras och det " +
                                "ska finnas en ansvarig person f�r att planen tas fram och justeras."
            });

            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 49,
                TreatmentTypeId = 7,
                Code = "GA024, Utprovning och f�rskrivning av utrustning som ej ing�r i hj�lpmedelsf�rteckningen (i)",
                Name = "Utprovning och f�rskrivning av utrustning som ej ing�r i hj�lpmedelsf�rteckningen (i)",
                Description = "T ex AKK (Alternativ och Kompletterande Kommunikation) eller oralmotoriska hj�lpmedel, inkl gomplatta."
            });

            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 50,
                TreatmentTypeId = 7,
                Code = "GA025, Utprovning och f�rskrivning av �vriga hj�lpmedel (exklusive rullstol) (i)",
                Name = "Utprovning och f�rskrivning av �vriga hj�lpmedel (exklusive rullstol) (i)",
                Description = "Information, utprovning, f�rskrivning, specialanpassning, tr�ning och uppf�ljning av hj�lpmedel som ing�r" +
                                " i hj�lpmedelsf�rteckningen, (exklusive rullstol)."
            });

            //Krysslista, TreatmentTypeId = 8. V�rdtj�nst(er): Bes�k p� mottagning
            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 51,
                TreatmentTypeId = 8,
                Code = "XS001, Information och r�dgivning med f�retr�dare f�r patienten (i)",
                Name = "Information och r�dgivning med f�retr�dare f�r patienten (i)",
                Description = "M�te d�r n�gon f�retr�dare f�r patienten (v�rdnadshavare, god man, " +
                                "anh�rig, n�rst�ende eller annan som patienten sj�lv utsett som f�retr�dare)" +
                                " m�ter h�lso- och sjukv�rdspersonal utan att patienten �r med. Samtalet ska" +
                                " journalf�ras."
            });

            //Krysslista, TreatmentTypeId = 9. V�rdtj�nst(er): Resurskr�vande bes�k barn och ungdom under 18 �r
            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 52,
                TreatmentTypeId = 9,
                Code = "AE020, Unders�kning av oralmotorik (i)",
                Name = "Unders�kning av oralmotorik (i)",
                Description = "Innefattar unders�kning av ett eller flera av f�ljande omr�den: orofacial anatomi, motorik och sensorik."
            });

            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 53,
                TreatmentTypeId = 9,
                Code = "AU120, Uppr�ttande av strukturerad v�rd- och omsorgsplan (i)",
                Name = "Uppr�ttande av strukturerad v�rd- och omsorgsplan (i)",
                Description = "Uppr�ttande/reviderande av skriftlig strukturerad v�rd- och omsorgsplan Planen ska om m�jligt utformas " +
                                "tillsammans med ber�rd individ. I planen ska beskrivas planerad och beslutad v�rd och omsorg. F�r �tg�rderna " +
                                "i planen ska anges m�l. Planen ska utv�rderas och ompr�vas. Den ska dokumenteras och det ska finnas en ansvarig " +
                                "person f�r att planen tas fram och justeras."
            });

            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 54,
                TreatmentTypeId = 9,
                Code = "AV045, Unders�kning av tal (i)",
                Name = "Unders�kning av tal (i)",
                Description = "Innefattar unders�kning av ett eller flera av f�ljande omr�den: andning, fonation, oralmotorik, " +
                                "talflyt, artikulation, prosodi och f�rst�elighet."
            });

            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 55,
                TreatmentTypeId = 9,
                Code = "AV105, Unders�kning av kommunikation (i)",
                Name = "Unders�kning av kommunikation (i)",
                Description = "F�rst�else och hantering av spr�kets �vergripande kontext. F�rm�ga till samspel t ex turtagning, " +
                                "initiativ och respons i samtal."
            });

            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 56,
                TreatmentTypeId = 9,
                Code = "AV108, Unders�kning av spr�k (i)",
                Name = "Unders�kning av spr�k (i)",
                Description = "Innefattar unders�kning av ett eller flera av f�ljande omr�den: spontantal, icke-verbal kommunikation, " +
                                "pragmatik, spr�kf�rst�else, auditiv f�rst�else, symbolf�rst�else, minne, fonologi, grammatik, lexikon, " +
                                "syntax, semantik, ben�mningsf�rm�ga, repetitionsf�rm�ga, spr�klig medvetenhet samt l�s- och skrivf�rm�ga."
            });

            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 57,
                TreatmentTypeId = 9,
                Code = "AV111, Unders�kning av sv�ljning, �t- och drickf�rm�ga (i)",
                Name = "Unders�kning av sv�ljning, �t- och drickf�rm�ga (i)",
                Description = "Innefattar unders�kning av ett eller flera av f�ljande omr�den: andning, r�st, orofacial anatomi, " +
                                "motorik och sensorik, oral och faryngeal sv�ljningsfas samt kompensatoriska �tg�rder."
            });

            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 58,
                TreatmentTypeId = 9,
                Code = "GA024, Utprovning och f�rskrivning av utrustning som ej ing�r i hj�lpmedelsf�rteckningen (i)",
                Name = "Utprovning och f�rskrivning av utrustning som ej ing�r i hj�lpmedelsf�rteckningen (i)",
                Description = "T ex AKK (Alternativ och Kompletterande Kommunikation) eller oralmotoriska hj�lpmedel, inkl gomplatta."
            });

            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 59,
                TreatmentTypeId = 9,
                Code = "GA025, Utprovning och f�rskrivning av �vriga hj�lpmedel (exklusive rullstol) (i)",
                Name = "Utprovning och f�rskrivning av �vriga hj�lpmedel (exklusive rullstol) (i)",
                Description = "Information, utprovning, f�rskrivning, specialanpassning, tr�ning och uppf�ljning av hj�lpmedel " +
                                "som ing�r i hj�lpmedelsf�rteckningen, (exklusive rullstol)."
            });

            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 60,
                TreatmentTypeId = 9,
                Code = "GB009, Information och undervisning riktad till patient (i)",
                Name = "Information och undervisning riktad till patient (i)",
                Description = "Information till och utbildning av patient av v�sentligt st�rre omfattning �n vad som f�rekommer " +
                                "vid ordin�ra kontakter, t ex sm�rtskola, astmaskola, KOL-skola, ryggskola eller strukturerade " +
                                "patientutbildningar av olika slag."
            });

            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 61,
                TreatmentTypeId = 9,
                Code = "GB010, Information och undervisning riktad till n�rst�ende (i)",
                Name = "Information och undervisning riktad till n�rst�ende (i)",
                Description = "Information till och utbildning av n�rst�ende av v�sentligt st�rre omfattning �n vad som f�rekommer " +
                                "vid ordin�ra kontakter, t ex sm�rtskola, astmaskola, KOL-skola, ryggskola eller strukturerade " +
                                "n�rst�endeutbildningar av olika slag."
            });

            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 62,
                TreatmentTypeId = 10,
                Code = "UX001, Nybes�k",
                Name = "Nybes�k",
                Description = "saknas"
            });

            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 63,
                TreatmentTypeId = 11,
                Code = "UV006, Tal- och spr�kutredning, p�b�rjad",
                Name = "Tal- och spr�kutredning, p�b�rjad",
                Description = "saknas"
            });

            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 64,
                TreatmentTypeId = 11,
                Code = "UV007, Tal- och spr�kutredning, p�g�ende",
                Name = "Tal- och spr�kutredning, p�g�ende",
                Description = "saknas"
            });

            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 65,
                TreatmentTypeId = 11,
                Code = "UV008, Tal- och spr�kutredning, avslutad",
                Name = "Tal- och spr�kutredning, avslutad",
                Description = "saknas"
            });

            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 66,
                TreatmentTypeId = 12,
                Code = "AV045, Unders�kning av tal (i)",
                Name = "Unders�kning av tal (i)",
                Description = "Innefattar unders�kning av ett eller flera av f�ljande omr�den: andning, fonation, oralmotorik, " +
                                "talflyt, artikulation, prosodi och f�rst�elighet"
            });

            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 67,
                TreatmentTypeId = 12,
                Code = "AV108, Unders�kning av spr�k (i)",
                Name = "Unders�kning av spr�k (i)",
                Description = "Innefattar unders�kning av ett eller flera av f�ljande omr�den: spontantal, icke-verbal kommunikation, " +
                                "pragmatik, spr�kf�rst�else, auditiv f�rst�else, symbolf�rst�else, minne, fonologi, grammatik, lexikon, " +
                                "syntax, semantik, ben�mningsf�rm�ga, repetitionsf�rm�ga, spr�klig medvetenhet samt l�s- och skrivf�rm�ga."
            });

            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 68,
                TreatmentTypeId = 12,
                Code = "GD003, Intyg omfattande (i)",
                Name = "Intyg omfattande (i)",
                Description = "T.ex. LUH, r�ttsintyg, intyg f�r bostadsanpassning eller annat intyg av motsvarande omfattning."
            });

            context.Treatments.AddOrUpdate(p => p.Id, new Treatment
            {
                Id = 69,
                TreatmentTypeId = 13,
                Code = "GB004, Patientrelaterad extern information och utbildning (i)",
                Name = "Patientrelaterad extern information och utbildning (i)",
                Description = "Avser dels �verrapportering av patient till annan enhet och dels information och/eller " +
                                "utbildning avseende en viss patients behov till externa enheter (=utanf�r den egna kliniken)."
            });

            context.Services.AddOrUpdate(s => s.Id, new Service
            {
                Id = 1,
                ServiceType = 1,
                DiagnosisCodeRemark = "Diagnoskod obligatorisk.",
                NoOfDiagnosisCodes = 2,
                TreatmentCodeRemark = "KV�-koder inte obligatoriska f�r att f� ers�ttning. V�rdgivaren rapporterar l�mpliga KV�-koder d�r s� �r m�jligt.",
                NoOfTreatmentCodes = 8,
                TreatmentTypeId = "1,2,3,4,6,6,6,6",
                NoOfCheckedTreatmentLists = 2,
                CheckedTreatmentTypeId = "7,8"
            });

            context.Services.AddOrUpdate(s => s.Id, new Service
            {
                Id = 2,
                ServiceType = 2,
                DiagnosisCodeRemark = "Diagnoskod obligatorisk.",
                NoOfDiagnosisCodes = 2,
                TreatmentCodeRemark = "KV�-koder inte obligatoriska f�r att f� ers�ttning. V�rdgivaren rapporterar l�mpliga KV�-koder d�r s� �r m�jligt.",
                NoOfTreatmentCodes = 9,
                TreatmentTypeId = "5,1,2,3,4,6,6,6,6",
                NoOfCheckedTreatmentLists = 2,
                CheckedTreatmentTypeId = "9,8"
            });

            context.Services.AddOrUpdate(s => s.Id, new Service
            {
                Id = 3,
                ServiceType = 3,
                DiagnosisCodeRemark = "Diagnoskod obligatorisk.",
                NoOfDiagnosisCodes = 2,
                TreatmentCodeRemark = "KV�-koder inte obligatoriska f�r att f� ers�ttning. V�rdgivaren rapporterar l�mpliga KV�-koder d�r s� �r m�jligt.",
                NoOfTreatmentCodes = 8,
                TreatmentTypeId = "1,2,3,4,6,6,6,6",
                NoOfCheckedTreatmentLists = 1,
                CheckedTreatmentTypeId = "10"
            });

            context.Services.AddOrUpdate(s => s.Id, new Service
            {
                Id = 4,
                ServiceType = 4,
                DiagnosisCodeRemark = "Diagnoskod obligatorisk.",
                NoOfDiagnosisCodes = 2,
                TreatmentCodeRemark = "KV�-koder inte obligatoriska f�r att f� ers�ttning. V�rdgivaren rapporterar l�mpliga KV�-koder d�r s� �r m�jligt.",
                NoOfTreatmentCodes = 8,
                TreatmentTypeId = "1,2,3,4,6,6,6,6",
                NoOfCheckedTreatmentLists = 1,
                CheckedTreatmentTypeId = "10"
            });

            context.Services.AddOrUpdate(s => s.Id, new Service
            {
                Id = 5,
                ServiceType = 5,
                DiagnosisCodeRemark = "Diagnoskod obligatorisk.",
                NoOfDiagnosisCodes = 2,
                TreatmentCodeRemark = "",
                NoOfTreatmentCodes = 8,
                TreatmentTypeId = "1,2,3,4,6,6,6,6",
                NoOfCheckedTreatmentLists = 3,
                CheckedTreatmentTypeId = "11,12,13"
            });

            context.SaveChanges();
        }
    }
}