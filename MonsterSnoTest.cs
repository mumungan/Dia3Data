using Turbo.Plugins.Default;
using System.Windows.Forms;
using Turbo.Plugins.Jack;
using Turbo.Plugins.Jack.DevTool.Logger;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Turbo.Plugins.Stone
{
    public class MonsterSnoTest : BasePlugin, IInGameWorldPainter
    {
//        public Keys HotKey { get; set; }
        public IFont DefaultTextFont { get; set; }
        public List<uint> Dmonsters { get; set; }

        public MonsterSnoTest()
        {
            Enabled = true;
//            HotKey = Keys.E;

        }

        public override void Load(IController hud)
        {
            base.Load(hud);
            DefaultTextFont = Hud.Render.CreateFont("tahoma", 6, 255, 255, 128, 0, false, false, 250, 0, 0, 0, true);

            Dmonsters = new List<uint>() {
            1522,   // 1522 	0.31	몰락자 혼종	    Fallen Mongrel	    FallenHound_C
            26030,  // 26030 	0.27	기괴살덩이	    Grotesque	        Corpulent_A
            26032,  // 26032 	0.27	피바람살덩이	Horror	            Corpulent_C
            26033,  // 26033 	0.27	흉악살덩이	    Abomination	        Corpulent_D
            26075,  // 26075 	0.04	몰락자	        Fallen	            FallenGrunt_A
            26079,  // 26079 	0.31	몰락자 사냥개	Fallen Hound	    FallenHound_A
            26082,  // 26082 	0.17	몰락자 영매	    Fallen Shaman	    FallenShaman_A
            26184,  // 26184 	0.2	    라쿠니 여사냥꾼	Lacuni Huntress	    LacuniFemale_A
            26188,  // 26188 	0.05	시체지렁이	    Corpse Worm	        Lamprey_A
            26244,  // 26244 	0.06	모래먼지 임프	Dust Imp	        Sandling_A
            26251,  // 26251    0.06	시체청소부	    Scavenger	        Scavenger_A
            26254,  // 26254 	0.09	해골 방패잡이	Skeletal Shieldbearer	Shield_Skeleton_A
            26255,  // 26255 	0.09	되돌아온 방패병	Returned Shieldman	    Shield_Skeleton_B
            26269,  // 26269 	0.06	해골 활잡이	    Skeletal Bowmaster	    SkeletonArcher_D
            26281,  // 26281 	0.1	    독기 어린 수호자Noxious Guardian	skeletonMage_Poison_A
            26285,  // 26285 	0.09	해골	        Skeleton	        Skeleton_A
            26287,  // 26287 	0.09	뼛조각 전사	    Bone Warrior	    Skeleton_C
            26290,  // 26290 	0.19	되돌아온 집행자	Returned ExecutionerSkeleton_twohander_B
            26359,  // 26359 	0.12	사악한 창조술사	Foul Conjurer	    TriuneSummoner_C
            26367,  // 26367 	0.31	어둠의 광전사	Dark Berserker	    Triune_Berserker_A
            26368,  // 26368 	0.15	어둠의 지옥개	Dark Hellion	    Triune_Summonable_A
            26385,  // 26385 	0.2	    마른나방	    Withermoth	        WitherMoth_A
            26427,  // 26427 	0.09	걸신들린 망자	Ravenous Dead	    ZombieSkinny_B
            26431,  // 26431 	0.15	굶주린 시체	    Hungry Corpse	    Zombie_B
            55490,  // 55490 	0.06	인간 전갈	    Tormented Stinger	Monstrosity_Scorpion_A
            59594,  // 59594 	0.31	라쿠니 난도질꾼	Lacuni Slasher	    LacuniMale_B
            59595,  // 59595 	0.31	한겨울 추적자	Wintersbane Stalker	LacuniMale_C
            71373,  // 71373 	0.29	역병의 전령	    Herald of Pestilence    creepMob_A
            92342,  // 92342 	0.49	무덤에서 파낸 거수	Disentombed Hulk	Unburied_C
            107723, // 107723   0.07	가시 마귀	    Quill Fiend	        QuillDemon_A
            118149, // 118149   0.18	타락한 천사	    Corrupted Angel	    Angel_Corrupt_A
            118151, // 118151   0.85	망치 군주	    Mallet Lord	        MalletDemon_A
            135522, // 135522   0.2	    사로잡힌 악몽	Enslaved Nightmare	HoodedNightmare_A
            135654, // 135654   0.23	폭군	        Oppressor	        BigRed_A
            136901, // 136901   0.46	몰락자 우두머리	Fallen Overlord	    FallenChampion_C
            136902, // 136902   0.04	몰락자 병사	    Fallen Grunt	    FallenGrunt_C
            136918, // 136918   0.17	몰락자 예언가	Fallen Prophet	    FallenShaman_C
            137005, // 137005   0.09	해골 침략자	    Skeletal Raider	    Shield_Skeleton_D
            137930, // 137930   0.02	부화한 새끼거미	Brood Hatchling	    Spiderling_B
            141397, // 141397   0.08	해골 사격수	    Skeletal Ranger	    SkeletonArcher_C
            171847, // 171847   0.05	타오르는 구울	Blazing Ghoul	    Ghoul_E
            179342, // 179342   0.08	어둠의 해골 궁수 Dark Skeletal Archer SkeletonArcher_F
            179947, // 179947   0.15	구리송곳니 잠복꾼 Copperfang Lurker	SnakeMan_Melee_C
            180761, // 180761   0.46	고산지 터벅발이	Highland Walker	WoodWraith_A_HighlandsVer
            190366, // 190366   0.15	한겨울 여사냥꾼	Wintersbane Huntress	LacuniFemale_C
            201749, // 201749   0.16	영혼 휘갈이	    Soul Lasher	        SoulRipper_B
            202855, // 202855   0.01	쉭쉭벌레	    Zap Worm	        electricEel_B
            208967, // 208967   0.06	불타는 새끼거미	Fiery Spiderling	Spider_Elemental_Fire_tesla_A
            213849, // 213849   0.19	핏빛혈족 망치주먹	Blood Clan Mauler	GoatMutant_Melee_B
            213850, // 213850   0.07	핏빛혈족 투창병	Blood Clan Impaler	GoatMutant_Ranged_B
            230704, // 230704   0.15	똬리술사	    Spellwinder	        SnakeMan_Caster_C
            239505, // 239505   0.38	수렁덩치 덫 사냥꾼	Bogan Trapper	x1_BogFamily_ranged_A
            262145, // 262145   0.85	응징자	        Punisher	        X1_Westmarch_Brute_A
            272287, // 272287   0.26	미늘 잠복꾼	    Barbed Lurker	    X1_Squigglet_A
            274231, // 274231   0.18	날개 달린 암살자 Winged Assassin	x1_Pand_LeaperAngel
            274471, // 274471   0.15	현사	        Exarch	            x1_sniperAngel_a
            279254, // 279254   0.16	어둠나방	    Darkmoth	        x1_WitherMoth_A
            284740, // 284740   0.13	원혼 깃든 병사	Revenant Soldier	x1_Skeleton_Westmarch_A
            289164, // 289164   0.05	오물 박쥐	    Vile Bat	        x1_WestmarchBat_A
            294128, // 294128   0.64	해골 야수	    Skeletal Beast	    x1_Beast_Skeleton_A
            311130, // 311130   0.06	소환된 궁수	    Summoned Archer	    x1_SkeletonArcher_Westmarch_Ghost_A
            317327, // 317327   0.15	소환된 방패병	Summoned Shield Guard	x1_Shield_Skeleton_Westmarch_Ghost_A
            326654, // 326654   0.15	충격 포탑	    Shock Tower	        X1_Pand_Ext_Ordnance_Tower_Shock_A
            364564, // 364564   0.06	가시 마귀	    Quill Fiend	        QuillDemon_FastAttack_A
            367909, // 367909   0.06	오물박쥐	    Vile Bat	        x1_LR_WestmarchBat_A
            409553, // 409553   0.06	서리 구더기	    Frost Maggot	    p4_Maggot_A
            411021, // 411021   0.69	빙하 거인	    Glacial Colossus	P4_Yeti_B
            418920, // 418920   0.15	섬뜩한 망령	    Grim Wraith     	p1_LR_Ghost_D
            418921, // 418921   0.15	죽음을 부르는 원혼	Deathly Haunt	p1_LR_Ghost_C
            418912, // 418912	0.15	격분한 환영	    Enraged Phantom	    p1_LR_Ghost_A
            429794, // 429794   1	    혐오스러운 수집가	Odious Collector	TreasureGoblin_B
            429795, // 429795   1	    보물 고블린	    Treasure Goblin	    TreasureGoblin_A
            433081, // 433081   0.15	얼음혈족 영매	Ice Clan Shaman	    P4_Ice_Goatman_Shaman_C
            433133, // 433133   0.19	얼음혈족 전사	Ice Clan Warrior	P4_Ice_Goatman_Melee_C
            433138, // 433138   0.11	얼음혈족 투창병	Ice Clan Impaler	P4_Ice_Goatman_Ranged_C
            461706, // 461706   0.11	핏빛 제자	    Hematic Disciple	p6_TempleCultist_Basic
            461711, // 461711   0.05	핏빛 광신도	    Hematic Zealot	    p6_TempleCultist_Special
            461713, // 461713   0.04	무덤 바퀴	    Tomb Roach	        p6_Beetle
            461716, // 461716   0.35	뒤틀린 숙주	    Wretched Host	    p6_TempleMonstrosity
            461709, // 461709	0.12	핏빛 사제	    Hematic Priest	    p6_TempleCultist_Caster
            294766, // 294766	0.02	풍뎅이	        Scarab	            x1_Monstrosity_ScorpionBug_A
            205769, // 205769	0.54	지옥가죽 울림귀	Hellhide Tremor	    Brickhouse_B
            175611, // 175611	0.19	끔찍한 요부	    Vile Temptress	    Succubus_B
            136865, // 136865	0.15	철갑 파괴자  	Armored Destroyer	CoreEliteDemon_A_NoPod
            136803, // 136803	0.09	사막 쐐기벌	    Dune Stinger	    Sandwasp_B
            26007,  // 26007	0.77	거대뿔 골리앗	Great Horned Goliath Beast_D
            123676, // 123676	0.31	덩치 큰 위상수	Hulking Phasebeast	azmodanBodyguard_A
            460756, // 460756	0.23	윤회자	        Reborn	            p6_Shepherd
            26306,  // 26306	0.15	지독한 벌레 떼	Vile Swarm	        Swarm_B
            26277,  // 26277	0.15	타오르는 수호자	Blazing Guardian	skeletonMage_Fire_A
            26267,  // 26267	0.08	해골 궁수	    Skeletal Archer	    SkeletonArcher_A
            155328, // 155328	0.17	몰락자 창조술사	Fallen Conjurer	    FallenShaman_B
            123685, // 123685	0.15	철갑 파괴자	    Armored Destroyer	CoreEliteDemon_A
            26109,  // 26109	0.19	달혈족 전사	    Moon Clan Warrior	Goatman_Moonclan_Melee_A
            358615, // 358615	3.08	역병촉수	    Blighter	        X1_LR_Boss_creepMob_A
            51457,  // 51457	0.73	사나운 야수	    Savage Beast	    Beast_A
            26429,  // 26429	0.23	걸어다니는 시체	Walking Corpse	    Zombie_A
            26423,  // 26423	0.14	구역질나는 송장	Retching Cadaver	ZombieFemale_B
            26375,  // 26375	0.49	시체덩어리	    Unburied	        Unburied_A
            26245,  // 26245	0.46	모래 괴수	    Sand Dweller	    SandMonster_A
            26185,  // 26185	0.2	    라쿠니 추적자	Lacuni Stalker	    LacuniFemale_B
            26113,  // 26113	0.17	달혈족 영매	    Moon Clan Shaman	Goatman_Moonclan_Shaman_A
            26111,  // 26111	0.09	달혈족 투창병	Moon Clan Impaler	Goatman_Moonclan_Ranged_A
            26109,  // 26109	0.19	달혈족 전사	    Moon Clan Warrior	Goatman_Moonclan_Melee_A
            26105,  // 26105	0.09	구울	        Ghoul	            Ghoul_A
            26088,  // 26088	0.07	시체 박쥐	    Carrion Bat	        FleshPitFlyer_A
            461675, // 461675	0.15	코르비안 사냥꾼	Corvian Hunter	    p6_RavenFlyer
            461700, // 461700	0.15	까마귀사냥개	Crowhound	        p6_CrowHound
            461717, // 461717	0.31	흰털 울음꾼	    Whitefur Howler	    P6_Werewolf_White
            461702, // 461702	0.31	밤의 울음꾼	    Night Howler	    P6_Werewolf_Black
            429793, // 429793	1	    보석 싹슬이꾼	Gem Hoarder	        TreasureGoblin_C
            311124, // 311124	0.07	소환된 병사	    Summoned Soldier	x1_Skeleton_Westmarch_Ghost_A
            290743, // 290743	0.09	원혼 깃든 궁수	Revenant Archer	    x1_SkeletonArcher_Westmarch_A
            204476, // 204476	0.09	모멸자	        Reviled	            fastMummy_C
            166397, // 166397	0.04	몰락자 악마	    Fallen Soldier	    FallenGrunt_D
            165380, // 165380	0.04	몰락자 날품팔이	Fallen Peon	        FallenGrunt_B
            55491,  // 55491	0.06	저승 땅전갈	    Stygian Crawler	    Monstrosity_Scorpion_B
            26286,  // 26286	0.09	되돌아온 자	    Returned	        Skeleton_B
            26284,  // 26284	0.15	되돌아온 소환사	Returned Summoner	SkeletonSummoner_B
            26116,  // 26116	0.07	핏빛혈족 창병	Blood Clan Spearman	GoatMutant_Ranged_A
            26115,  // 26115	0.19	핏빛혈족 전사	Blood Clan Warrior	GoatMutant_Melee_A
            26080,  // 26080	0.31	몰락자 잡종개	Fallen Cur	        FallenHound_B
            26033,  // 26033	0.27	흉악살덩이	    Abomination	        Corpulent_D
            26250,  // 26250	0.09	모래 말벌	    Sand Wasp	        Sandwasp_A
            26252,  // 26252	0.05	수확꾼	        Reaper	            Scavenger_C
            26268,  // 26268	0.08	되돌아온 궁수	Returned Archer	    SkeletonArcher_B
            26278,  // 26278	0.15	그을린 피조물	Smoldering Construct	skeletonMage_Fire_B
            26279,  // 26279	0.06	번개충격 수호자	Shock Guardian	    skeletonMage_Lightning_A
            26296,  // 26296	0.15	비틀린 몸의 기만자	Writhing Deceiver	SnakeMan_Melee_A
            26297,  // 26297	0.15	파멸의 독사	    Doom Viper	        SnakeMan_Melee_B
            26348,  // 26348	0.06	어둠의 이교도	Dark Cultist	    TriuneCultist_A
            26420,  // 26420	0.01	굶주린 몸뚱이	Hungry Torso	    ZombieCrawler_B
            63347,  // 63347	0.09	게걸스러운 좀비	Voracious Zombie	ZombieSkinny_C
            107951, // 107951	0.06	얼음 가시등	    Icy Quillback	    QuillDemon_C
            138874, // 138874	0.15	제압자	        Subjugator	        MastaBlasta_Rider_A
            138875, // 138875	0.46	아르마돈	    Armaddon	        MastaBlasta_Steed_A
            170301, // 170301	0.31	어그러진 피바람 악마	Warping Horror	azmodanBodyguard_B
            170914, // 170914	0.02	광포한 미치광이	Frenzied Lunatic	FallenLunatic_D
            187235, // 187235	0.09	타오르는 칼잡이	Blazing Swordwielder	Skeleton_D_Fire
            219805, // 219805	0.19	지옥 마녀	    Hell Witch	        Succubus_C
            223688, // 223688	0.05	척추 휘갈이	    Spine Lasher	    BileCrawler_B
            237371, // 237371	0.03	수렁도치	    Boggit	            x1_BogFamily_melee_A
            238536, // 238536	0.54	뿔엄니 수렁덩치	Tusked Bogan	    x1_BogFamily_brute_A
            272415, // 272415	0.15	공포의 울음	    Shrieking Terror	x1_NightScreamer_A
            289861, // 289861	0.31	섬뜩한 천사	    Ghastly Seraph	    x1_westmarchRanged_A
            418903, // 418903	0.77	구더기 군체	    Maggot Brood	    p1_LR_BogBlight_A
            418919, // 418919	0.15	끔찍한 혼백	    Vile Revenant	    p1_LR_Ghost_B
            170274, // 170274	0.12	악마 침략자	    Demon Raider	    demonTrooper_B
            273413, // 273413	0.58	죽음의 시녀	    Death Maiden	    x1_DeathMaiden_A
            249504, // 249504	0.31	혼란마	        Anarch	            x1_Wraith_A
            332680, // 332680	0.85	집행자	        Executioner     	X1_Westmarch_Brute_C
            26010,  // 26010	0.05	한 맺힌 몸덩이	Bile Crawler	    BileCrawler_A
            55727,  // 55727	0.15	몰루 침략자	    Morlu Invader	    MorluMelee_A
            55728,  // 55728    0.15	몰루 군단병	    Morlu Legionnaire	MorluMelee_B
            55731,  // 55731	0.27	몰루 소각술사	Morlu Incinerator	MorluSpellcaster_A
            82765,  // 82765    0.01	음침한 망령	    Gloom Wraith	    shadowVermin_B
            118153, // 118153	0.23	공포의 악마	    Terror Demon	    TerrorDemon_A
            143937, // 143937	0.38	기동 아르마돈	Mounted Armaddon	MastaBlasta_Combined_A
            199475, // 199475	0.01	그늘 추적자     Shade Stalker	    shadowVermin_C_Spire
            218952, // 218952	0.04	거대한 그늘날개	Giant Shadow Wing	demonFlyer_C_bomber
            223418, // 223418	0.23	악마불 몽마	    Demonfire Nightmare	a4dun_spire_firewallMonster
            2224637,// 2224637	0.03	한 맺힌 몸덩이	Bile Crawler	    BileCrawler_A_Large_Aggro
            225258, // 225258	0.01	척추 휘갈이	    Spine Lasher	    BileCrawler_B_Large_Aggro
            2429785,// 2429785	1	    악랄한 고문관	Malevolent Tormentor	TreasureGoblin_H
            26005,  // 26005	0.73	뿔 돌격수	    Horned Charger	    Beast_B
            26283,  // 26283	0.15	무덤 수호자	    Tomb Guardian	    SkeletonSummoner_A
            26299,  // 26299	0.23	피바람 거미	    Arachnid Horror	    Spider_A
            26419,  // 26419	0.01	기어다니는 몸뚱이	Crawling Torso	ZombieCrawler_A

            };
        }

        public void PaintWorld(WorldLayer layer)
        {
 //           if (!Hud.Input.IsKeyDown(HotKey)) return;
            var monsters = Hud.Game.AliveMonsters;
            string text1 = "";       
            if(Hud.Game.CurrentGameTick % 60.0f == 0) 
            {
            foreach (var monster in monsters)
            {

               if (Dmonsters.Contains(monster.SnoMonster.Sno)) return;
                text1 = "MonstersData:\n";
                text1 += monster.SnoMonster.Sno + "\t";
                text1 += (monster.SnoMonster.RiftProgression * 100.0d / this.Hud.Game.MaxQuestProgress).ToString("N2") + "\t"; //650point == 100% //RiftProgression(point)
                text1 += monster.SnoMonster.NameLocalized + "\t";
                text1 += monster.SnoMonster.NameEnglish + "\t";
                text1 += monster.SnoMonster.Code + "\n";
                Hud.TextLog.Log("monsterdic", text1);


            }
//          var text1 = string.Format("{0}", );
            var layer1 = DefaultTextFont.GetTextLayout(text1);
            DefaultTextFont.DrawText(layer1, Hud.Window.Size.Width * 0.2f, Hud.Window.Size.Height * 0.05f);

//          Says.Debug(text1);
            }
        }
    }
}




