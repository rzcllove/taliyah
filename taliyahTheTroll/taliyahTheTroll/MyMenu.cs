using System.Linq;
using System.Security.Cryptography.X509Certificates;
 using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using SharpDX;


namespace taliyahTheTroll
{
    internal static class TalliyahTheTrollMeNu
    {
        private static Menu _myMenu;
        public static Menu ComboMenu, DrawMeNu, HarassMeNu, Activator, FarmMeNu, MiscMeNu;

        public static void LoadMenu()
        {
            MyTalliyahTheTrollPage();
            ComboMenuPage();
            FarmMeNuPage();
            HarassMeNuPage();
            ActivatorPage();
            MiscMeNuPage();
            DrawMeNuPage();
        }

        private static void MyTalliyahTheTrollPage()
        {
            _myMenu = MainMenu.AddMenu("Taliyah The Troll", "main");
            _myMenu.AddLabel(" Taliyah The Troll " + Program.Version);
            _myMenu.AddLabel(" Made by MeLoDag");
        }

        private static void DrawMeNuPage()
        {
            DrawMeNu = _myMenu.AddSubMenu("Draw  settings", "Draw");
            DrawMeNu.AddGroupLabel("Draw Settings:");
            DrawMeNu.Add("nodraw",
                new CheckBox("No Display Drawing", false));
          DrawMeNu.AddSeparator();
            DrawMeNu.Add("draw.Q",
                new CheckBox("Draw Q"));
            DrawMeNu.Add("draw.W",
                new CheckBox("Draw W"));
            DrawMeNu.Add("draw.E",
                new CheckBox("Draw E"));
            DrawMeNu.Add("draw.R",
                new CheckBox("Draw R"));
            DrawMeNu.AddLabel("Damage indicators");
            DrawMeNu.Add("healthbar", new CheckBox("Healthbar overlay"));
            DrawMeNu.Add("percent", new CheckBox("Damage percent info"));
        }

        private static void ComboMenuPage()
        {
            ComboMenu = _myMenu.AddSubMenu("Combo settings", "Combo");
            ComboMenu.AddGroupLabel("Combo settings:");
            ComboMenu.AddLabel("Use Q  Spell on");
            foreach (var enemies in EntityManager.Heroes.Enemies.Where(i => !i.IsMe))
            {
                ComboMenu.Add("combo.Q" + enemies.ChampionName, new CheckBox("" + enemies.ChampionName));
            }
            ComboMenu.AddSeparator();
            ComboMenu.Add("combo.E",
                new CheckBox("Use E"));
            ComboMenu.Add("combo.w",
                new CheckBox("Use W"));
            ComboMenu.AddSeparator();
            ComboMenu.AddGroupLabel("Combo preferences:");
            ComboMenu.Add("use.onlyq5", new CheckBox("Use Q Only If 5X Q",false));
            ComboMenu.Add("combo.CCQ",
                new CheckBox("Use Q CC"));
          }


        private static void FarmMeNuPage()
        {
            FarmMeNu = _myMenu.AddSubMenu("Lane Clear Settings", "laneclear");
            FarmMeNu.AddGroupLabel("Lane clear settings:");
            FarmMeNu.Add("Lane.Q",
                new CheckBox("Use Q"));
           FarmMeNu.Add("LaneMana",
                new Slider("Min. Mana for Laneclear Spells %", 60));
            FarmMeNu.AddSeparator();
            FarmMeNu.AddGroupLabel("Jungle Settings");
            FarmMeNu.Add("jungle.Q",
                new CheckBox("Use Q"));
         }

        private static void HarassMeNuPage()
        {
            HarassMeNu = _myMenu.AddSubMenu("Harass/Killsteal Settings", "hksettings");
            HarassMeNu.AddGroupLabel("Harass Settings:");
            HarassMeNu.AddSeparator();
            HarassMeNu.AddLabel("Use Q on");
            foreach (var enemies in EntityManager.Heroes.Enemies.Where(i => !i.IsMe))
            {
                HarassMeNu.Add("Harass.Q" + enemies.ChampionName, new CheckBox("" + enemies.ChampionName));
            }
            HarassMeNu.Add("harass.QE",
                new Slider("Min. Mana for Harass Spells %", 55));
            HarassMeNu.AddSeparator();
            HarassMeNu.AddGroupLabel("KillSteal Settings:");
            HarassMeNu.Add("killsteal.Q",
                new CheckBox("Use Q", false));
        }

        private static void ActivatorPage()
        {
            Activator = _myMenu.AddSubMenu("Activator Settings", "Items");
            Activator.AddLabel("Zhonyas Settings");
            Activator.Add("Zhonyas", new CheckBox("Use Zhonyas Hourglass"));
            Activator.Add("ZhonyasHp", new Slider("Use Zhonyas Hourglass If Your HP%", 20, 0, 100));
            Activator.AddGroupLabel("Potion Settings");
            Activator.Add("spells.Potions.Check",
                new CheckBox("Use Potions"));
            Activator.Add("spells.Potions.HP",
                new Slider("Use Potions when HP is lower than {0}(%)", 60, 1));
            Activator.Add("spells.Potions.Mana",
                new Slider("Use Potions when Mana is lower than {0}(%)", 60, 1));
            Activator.AddSeparator();
            Activator.AddGroupLabel("Spells settings:");
            Activator.AddGroupLabel("Heal settings:");
            Activator.Add("spells.Heal.Hp",
                new Slider("Use Heal when HP is lower than {0}(%)", 30, 1));
            Activator.AddGroupLabel("Ignite settings:");
            Activator.Add("spells.Ignite.Focus",
                new Slider("Use Ignite when target HP is lower than {0}(%)", 10, 1));
        }

        private static void MiscMeNuPage()
        {
            MiscMeNu = _myMenu.AddSubMenu("Misc Menu", "othermenu");
            MiscMeNu.AddGroupLabel("Settings for Flee");
           
            MiscMeNu.Add("gapcloser.E",
                new CheckBox("Use E GapCloser"));
            MiscMeNu.Add("interupt.W",
              new CheckBox("Use W Interrupt"));
            MiscMeNu.AddSeparator();
            MiscMeNu.AddGroupLabel("Skin settings");
            MiscMeNu.Add("checkSkin",
                new CheckBox("Use skin changer:", false));
            MiscMeNu.Add("skin.Id",
                new Slider("Skin Editor", 5, 0, 10));
            
            foreach (var enemy in EntityManager.Heroes.Enemies.Where(i => !i.IsMe))
            {
                    foreach (
                        var spell in
                            enemy.Spellbook.Spells.Where(
                                a =>
                                    a.Slot == SpellSlot.Q || a.Slot == SpellSlot.W || a.Slot == SpellSlot.E ||
                                    a.Slot == SpellSlot.R))
                    {
                        if (spell.Slot == SpellSlot.Q)
                        {
                            if(enemy.ChampionName == "Thresh")
                            {
                            HarassMeNu.Add("ThreshQLeap",
                                new CheckBox(enemy.ChampionName + " - Q - " + spell.Name, true));
                            MiscMeNu.Add("ThreshQLeap",
                                new CheckBox(enemy.ChampionName + " - Q - " + spell.Name, true)); 
                            }
                            else if(enemy.ChampionName == "Elise")
                            {
                            HarassMeNu.Add("EliseHumanQ",
                                new CheckBox(enemy.ChampionName + " - Q - " + spell.Name, true));
                            MiscMeNu.Add("EliseHumanQ",
                                new CheckBox(enemy.ChampionName + " - Q - " + spell.Name, true));
                            HarassMeNu.Add("EliseSpiderQLast",
                                new CheckBox(enemy.ChampionName + " - Q - " + spell.Name, true));
                            MiscMeNu.Add("EliseSpiderQLast",
                                new CheckBox(enemy.ChampionName + " - Q - " + spell.Name, true));
                            }
                            
                           else
                           {
                            HarassMeNu.Add(spell.SData.Name,
                                new CheckBox(enemy.ChampionName + " - Q - " + spell.Name, false));
                            MiscMeNu.Add(spell.SData.Name,
                                new CheckBox(enemy.ChampionName + " - Q - " + spell.Name, false));
                           }
                            
                        }
                        else if (spell.Slot == SpellSlot.W)
                        {
                            if(enemy.ChampionName == "Leblanc")
                            {
                            HarassMeNu.Add("leblancslidereturn",
                                new CheckBox(enemy.ChampionName + " - W - " + spell.Name, true));
                            MiscMeNu.Add("leblancslidereturn",
                                new CheckBox(enemy.ChampionName + " - W - " + spell.Name, true)); 
                            HarassMeNu.Add("leblancslidereturnM",
                                new CheckBox(enemy.ChampionName + " - W - " + spell.Name, true));
                            MiscMeNu.Add("leblancslidereturnM",
                                new CheckBox(enemy.ChampionName + " - W - " + spell.Name, true)); 
                            }
                            else if(enemy.ChampionName == "Zed")
                            {
                            HarassMeNu.Add("ZedW2",
                                new CheckBox(enemy.ChampionName + " - W - " + spell.Name, true));
                            MiscMeNu.Add("ZedW2",
                                new CheckBox(enemy.ChampionName + " - W - " + spell.Name, true)); 

                            }
                            else if(enemy.ChampionName == "Thresh")
                            {
                            HarassMeNu.Add("LanternWAlly",
                                new CheckBox(enemy.ChampionName + " - W - " + spell.Name, true));
                            MiscMeNu.Add("LanternWAlly",
                                new CheckBox(enemy.ChampionName + " - W - " + spell.Name, true)); 
                            }
                            else if(enemy.ChampionName == "Elise")
                            {
                            HarassMeNu.Add("EliseHumanW",
                                new CheckBox(enemy.ChampionName + " - W - " + spell.Name, true));
                            MiscMeNu.Add("EliseHumanW",
                                new CheckBox(enemy.ChampionName + " - W - " + spell.Name, true)); 
                            }
                            else
                            {
                            HarassMeNu.Add(spell.SData.Name,
                                new CheckBox(enemy.ChampionName + " - W - " + spell.Name, false));
                            MiscMeNu.Add(spell.SData.Name,
                                new CheckBox(enemy.ChampionName + " - W - " + spell.Name, false));     
                            }    
                        }
                        else if (spell.Slot == SpellSlot.E)
                        {
                            if(enemy.ChampionName == "Fizz")
                            {
                            HarassMeNu.Add("FizzJumpTwo",
                                new CheckBox(enemy.ChampionName + " - E - " + spell.Name, true));
                            MiscMeNu.Add("FizzJumpTwo",
                                new CheckBox(enemy.ChampionName + " - E - " + spell.Name, true));
                            }  
                            else if(enemy.ChampionName == "Elise")
                            {
                            HarassMeNu.Add("EliseSpiderEDescent",
                                new CheckBox(enemy.ChampionName + " - E - " + spell.Name, true));
                            MiscMeNu.Add("EliseSpiderEDescent",
                                new CheckBox(enemy.ChampionName + " - E - " + spell.Name, true));
                            HarassMeNu.Add("EliseHumanE",
                                new CheckBox(enemy.ChampionName + " - E - " + spell.Name, true));
                            MiscMeNu.Add("EliseHumanE",
                                new CheckBox(enemy.ChampionName + " - E - " + spell.Name, true));
                            }  
                            else
                            {
                            HarassMeNu.Add(spell.SData.Name,
                                new CheckBox(enemy.ChampionName + " - E - " + spell.Name, false));
                            MiscMeNu.Add(spell.SData.Name,
                                new CheckBox(enemy.ChampionName + " - E - " + spell.Name, false));
                            }    
                        }
                        else if (spell.Slot == SpellSlot.R)
                        {
                            if(enemy.ChampionName == "Zed")
                            {
                            HarassMeNu.Add("ZedR2",
                                new CheckBox(enemy.ChampionName + " - R - " + spell.Name, true));
                            MiscMeNu.Add("ZedR2",
                                new CheckBox(enemy.ChampionName + " - R - " + spell.Name, true)); 
                            }    
                            else
                            {
                            HarassMeNu.Add(spell.SData.Name,
                                new CheckBox(enemy.ChampionName + " - R - " + spell.Name, false));
                            MiscMeNu.Add(spell.SData.Name,
                                new CheckBox(enemy.ChampionName + " - R - " + spell.Name, false));
                            }        
                        }
                    }
            }
            
        }

        public static bool Nodraw()
        {
            return DrawMeNu["nodraw"].Cast<CheckBox>().CurrentValue;
        }
        
        public static bool DrawingsQ()
        {
            return DrawMeNu["draw.Q"].Cast<CheckBox>().CurrentValue;
        }

        public static bool DrawingsW()
        {
            return DrawMeNu["draw.W"].Cast<CheckBox>().CurrentValue;
        }

        public static bool DrawingsE()
        {
            return DrawMeNu["draw.E"].Cast<CheckBox>().CurrentValue;
        }

        public static bool DrawingsR()
        {
            return DrawMeNu["draw.R"].Cast<CheckBox>().CurrentValue;
        }

        public static bool DrawingsT()
        {
            return DrawMeNu["draw.T"].Cast<CheckBox>().CurrentValue;
        }

        public static bool ComboE()
        {
            return ComboMenu["combo.E"].Cast<CheckBox>().CurrentValue;
        }

        public static bool ComboW()
        {
            return ComboMenu["combo.W"].Cast<CheckBox>().CurrentValue;
        }

        public static bool UseQonly5()
        {
            return ComboMenu["use.onlyq5"].Cast<CheckBox>().CurrentValue;
        } 
        public static bool LaneQ()
        {
            return FarmMeNu["lane.Q"].Cast<CheckBox>().CurrentValue;
        }
       public static float LaneMana()
        {
            return FarmMeNu["LaneMana"].Cast<Slider>().CurrentValue;
        }

        public static bool JungleQ()
        {
            return FarmMeNu["jungle.Q"].Cast<CheckBox>().CurrentValue;
        }
      public static bool HarassQ()
        {
            return HarassMeNu["harass.Q"].Cast<CheckBox>().CurrentValue;
        }

        public static float HarassQe()
        {
            return HarassMeNu["harass.QE"].Cast<Slider>().CurrentValue;
        }

        public static bool KillstealQ()
        {
            return HarassMeNu["killsteal.Q"].Cast<CheckBox>().CurrentValue;
        }

        public static bool SpellsPotionsCheck()
        {
            return Activator["spells.Potions.Check"].Cast<CheckBox>().CurrentValue;
            
        }
        public static float SpellsPotionsHp()
        {
            return Activator["spells.Potions.HP"].Cast<Slider>().CurrentValue;
        }

        public static float SpellsPotionsM()
        {
            return Activator["spells.Potions.Mana"].Cast<Slider>().CurrentValue;
        }

        public static float SpellsHealHp()
        {
            return Activator["spells.Heal.HP"].Cast<Slider>().CurrentValue;
        }

        public static float SpellsIgniteFocus()
        {
            return Activator["spells.Ignite.Focus"].Cast<Slider>().CurrentValue;
        }
        
        public static int SkinId()
        {
            return MiscMeNu["skin.Id"].Cast<Slider>().CurrentValue;
        }

        public static bool GapcloserE()
        {
            return MiscMeNu["gapcloser.E"].Cast<CheckBox>().CurrentValue;
        }
        
        public static bool InterupteW()
        {
            return MiscMeNu["interupt.W"].Cast<CheckBox>().CurrentValue;
        }

        public static bool SkinChanger()
        {
            return MiscMeNu["SkinChanger"].Cast<CheckBox>().CurrentValue;
        }

        public static bool CheckSkin()
        {
            return MiscMeNu["checkSkin"].Cast<CheckBox>().CurrentValue;
        }
    }
}
