﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using System.Globalization;

namespace BF2Statistics
{
    public partial class ServerSettingsForm : Form
    {
        /// <summary>
        /// Our Settings Object, which contains all of our settings
        /// </summary>
        private ServerSettings Settings;

        /// <summary>
        /// Indicates whether we are using a global settings file
        /// </summary>
        private bool UseGlobalSettings;

        /// <summary>
        /// The contents of the AiDefault.ai file
        /// </summary>
        private string AiDefaultText = "";

        /// <summary>
        /// An array of aiSettings matches that will need replaced in the AiDefault file
        /// </summary>
        private string[] AiMatches = new string[4];

        /// <summary>
        /// Indicates whether the bot count was forced previously
        /// via the AiDefault.ai file
        /// </summary>
        private bool BotCountForced;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="UseGlobalSettings">
        /// If true, the global settings file will be used
        /// instead of the Selected mods settings file
        /// </param>
        public ServerSettingsForm(bool UseGlobalSettings)
        {
            InitializeComponent();
            this.UseGlobalSettings = UseGlobalSettings;

            // Parse settings file, and fill in form values
            try
            {
                // Load Settings, and set for title
                if (UseGlobalSettings)
                {
                    this.Settings = new ServerSettings(Path.Combine(Program.RootPath, "Python", "GlobalServerSettings.con"));
                    this.Text = "Global Server Settings";
                }
                else
                {
                    this.Settings = MainForm.SelectedMod.ServerSettings;
                    this.Text = MainForm.SelectedMod.Title + " Server Settings";
                }

                // General
                ServerNameBox.Text = Settings.GetValue("serverName", "Default Server Name");
                ServerPasswordBox.Text = Settings.GetValue("password", "");
                ServerIpBox.Text = Settings.GetValue("serverIP", "");
                ServerPortBox.Value = Int32.Parse(Settings.GetValue("serverPort", "16567"));
                GamespyPortBox.Value = Int32.Parse(Settings.GetValue("gameSpyPort", "29900"));
                ServerWelcomeBox.Text = Settings.GetValue("welcomeMessage", "");
                AutoBalanceBox.Checked = (Int32.Parse(Settings.GetValue("autoBalanceTeam", "0")) == 1);
                EnablePublicServerBox.Checked = (Int32.Parse(Settings.GetValue("internet", "0")) == 1);
                EnablePunkBuster.Checked = (Int32.Parse(Settings.GetValue("punkBuster", "0")) == 1);
                RoundsPerMapBox.Value = Int32.Parse(Settings.GetValue("roundsPerMap", "3"));
                PlayersToStartSlider.Value = Int32.Parse(Settings.GetValue("numPlayersNeededToStart", "2"));
                MaxPlayersBar.Value = Int32.Parse(Settings.GetValue("maxPlayers", "16"));
                TicketRatioBar.Value = Int32.Parse(Settings.GetValue("ticketRatio", "200"));
                ScoreLimitBar.Value = Int32.Parse(Settings.GetValue("scoreLimit", "0"));
                TeamRatioBar.Value = Int32.Parse(Settings.GetValue("teamRatioPercent", "50"));

                // Time settings
                TimeLimitBar.Value = Int32.Parse(Settings.GetValue("timeLimit", "0"));
                SpawnTimeBar.Value = Int32.Parse(Settings.GetValue("spawnTime", "15"));
                ManDownBar.Value = Int32.Parse(Settings.GetValue("manDownTime", "15"));
                StartDelayBar.Value = Int32.Parse(Settings.GetValue("startDelay", "15"));
                EndDelayBar.Value = Int32.Parse(Settings.GetValue("endDelay", "15"));
                EORBar.Value = Int32.Parse(Settings.GetValue("endOfRoundDelay", "15"));
                NotEnoughPlayersBar.Value = Int32.Parse(Settings.GetValue("notEnoughPlayersRestartDelay", "15"));
                TimeB4RestartMapBar.Value = Int32.Parse(Settings.GetValue("timeBeforeRestartMap", "45"));

                // Friendly Fire Settings
                PunishTeamKillsBox.Checked = (Int32.Parse(Settings.GetValue("tkPunishEnabled", "1")) == 1);
                FriendlyFireBox.Checked = (Int32.Parse(Settings.GetValue("friendlyFireWithMines", "0")) == 1);
                PunishDefaultBox.Checked = (Int32.Parse(Settings.GetValue("tkPunishByDefault", "0")) == 1);
                TksBeforeKickBox.Value = Int32.Parse(Settings.GetValue("tkNumPunishToKick", "3"));
                SoldierFFBar.Value = Int32.Parse(Settings.GetValue("soldierFriendlyFire", "100"));
                VehicleFFBar.Value = Int32.Parse(Settings.GetValue("vehicleFriendlyFire", "100"));
                SoldierSplashFFBar.Value = Int32.Parse(Settings.GetValue("soldierSplashFriendlyFire", "100"));
                VehicleSplashFFBar.Value = Int32.Parse(Settings.GetValue("vehicleSplashFriendlyFire", "100"));

                // Voip
                EnableVoip.Checked = (Int32.Parse(Settings.GetValue("voipEnabled", "1")) == 1);
                EnableRemoteVoip.Checked = (Int32.Parse(Settings.GetValue("voipServerRemote", "0")) == 1);
                VoipBF2ClientPort.Value = Int32.Parse(Settings.GetValue("voipBFClientPort", "55123"));
                VoipBF2ServerPort.Value = Int32.Parse(Settings.GetValue("voipBFServerPort", "55124"));
                VoipServerPort.Value = Int32.Parse(Settings.GetValue("voipServerPort", "55125"));
                RemoteVoipIpBox.Text = Settings.GetValue("voipServerRemoteIP", "");
                VoipPasswordBox.Text = Settings.GetValue("voipSharedPassword", "");
                VoipQualityBar.Value = Int32.Parse(Settings.GetValue("voipQuality", "3"));

                // Voting Settings
                EnableVotingBox.Checked = (Int32.Parse(Settings.GetValue("votingEnabled", "1")) == 1);
                EnableTeamVotingBox.Checked = (Int32.Parse(Settings.GetValue("teamVoteOnly", "1")) == 1);
                VoteTimeBar.Value = Int32.Parse(Settings.GetValue("voteTime", "90"));
                PlayersVotingBar.Value = Int32.Parse(Settings.GetValue("minPlayersForVoting", "2"));

                // Demo & Urls
                EnableAutoRecord.Checked = (Int32.Parse(Settings.GetValue("autoRecord", "0")) == 1);
                DemoQualityBar.Value = Int32.Parse(Settings.GetValue("demoQuality", "5"));
                DemoIndexUrlBox.Text = Settings.GetValue("demoIndexURL", "http://");
                DemoDownloadBox.Text = Settings.GetValue("demoDownloadURL", "http://");
                DemoHookBox.Text = Settings.GetValue("autoDemoHook", "adminutils/demo/rotate_demo.exe");
                CLogoUrlBox.Text = Settings.GetValue("communityLogoURL", "");
                SLogoUrlBox.Text = Settings.GetValue("sponsorLogoURL", "");

                // Misc Settings
                AllowNATNagotiation.Checked = (Int32.Parse(Settings.GetValue("allowNATNegotiation", "0")) == 1);
                AllowFreeCam.Checked = (Int32.Parse(Settings.GetValue("allowFreeCam", "0")) == 1);
                AllowNoseCam.Checked = (Int32.Parse(Settings.GetValue("allowNoseCam", "1")) == 1);
                AllowExtViews.Checked = (Int32.Parse(Settings.GetValue("allowExternalViews", "1")) == 1);
                HitIndicatorEnabled.Checked = (Int32.Parse(Settings.GetValue("hitIndicator", "1")) == 1);
                RadioSpamIntBox.Value = Int32.Parse(Settings.GetValue("radioSpamInterval", "6"));
                RadioMaxSpamBox.Value = Int32.Parse(Settings.GetValue("radioMaxSpamFlagCount", "6"));
                RadioBlockTimeBar.Value = Int32.Parse(Settings.GetValue("radioBlockedDurationTime", "30"));

                // Bot Settings
                GetBotSettings();
            }
            catch (Exception e)
            {
                this.Load += (s, ea) => this.Close();
                MessageBox.Show(e.Message, "Server Settings File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Loads the bot settings from the Settings object and the /ai/aidefault.ai file
        /// </summary>
        protected void GetBotSettings()
        {
            // Get the Bot Count
            int BotCount = Int32.Parse(Settings.GetValue("coopBotCount", "16"));
            if (BotCount > 48)
                BotCount = 48;

            // Set form values
            BotCountBar.Value = BotCount;
            BotRatioBar.Value = Int32.Parse(Settings.GetValue("coopBotRatio", "50"));
            BotDifficultyBar.Value = Int32.Parse(Settings.GetValue("coopBotDifficulty", "70"));

            // Global settings does not have a aidefault.ai file
            if (UseGlobalSettings)
            {
                ForceBotCount.Enabled = false;
                return;
            }

            // Load the mods AiDefault.ai file
            try
            {
                AiDefaultText = File.ReadAllText(Path.Combine(MainForm.SelectedMod.RootPath, "ai", "AiDefault.ai"));
                if (!AiDefaultText.StartsWith("rem BF2Statistics Formatted"))
                    AiDefaultText = Program.GetResourceAsString("BF2Statistics.Resources.Ai.AIDefault.ai");
            }
            catch (Exception e)
            {
                Program.ErrorLog.Write("Warning: [ServerSettings] Could not open the AiDefault.ai file : " + e.Message);
                ForceBotCount.Enabled = false;
                return;
            }

            // Use regex to parse the settings
            int MaxBots = 16;
            Regex Reg = new Regex(@"aiSettings.(?<name>[A-Za-z]+)[\s|\t]+(?<value>[.0-9]+)");
            MatchCollection Matches = Reg.Matches(AiDefaultText);
            foreach (Match M in Matches)
            {
                switch (M.Groups["name"].Value.ToLower())
                {
                    case "overridemenusettings":
                        ForceBotCount.Checked = BotCountForced = (M.Groups["value"].Value.Trim() == "1");
                        AiMatches[0] = M.Value;
                        break;
                    case "setmaxnbots":
                        Int32.TryParse(M.Groups["value"].Value, out MaxBots);
                        AiMatches[1] = M.Value;
                        break;
                    case "setbotskill":
                        AiMatches[2] = M.Value;
                        break;
                    case "maxbotsincludehumans":
                        AiMatches[3] = M.Value;
                        break;
                }
            }

            // Make sure Team 1's bot count is not higher then the total bot count
            if (MaxBots < BotCount)
                MaxBots = BotCount;

            // Set values
            int Count = MaxBots - BotCount;
            BotCountBar1.Value = ((Count > 48) ? 48 : Count);
            BotCountBar2.Value = BotCount;
        }

        /// <summary>
        /// Sets the bot settings based on whether the ForceBotCount is checked
        /// </summary>
        protected void SetBotSettings()
        {
            // Save settings
            Settings.SetValue("coopBotDifficulty", BotDifficultyBar.Value.ToString());

            // If we are using global settings file, or not using the AiDefault.ai file
            if (UseGlobalSettings || !ForceBotCount.Checked)
            {
                Settings.SetValue("coopBotRatio", BotRatioBar.Value.ToString());
                Settings.SetValue("coopBotCount", BotCountBar.Value.ToString());

                // Stopping point for global settings
                if (UseGlobalSettings)
                    return;
            }
            else
            {
                // Forcing AIDefault, set ratio to 100 and the bot count for team 2
                Settings.SetValue("coopBotRatio", "100");
                Settings.SetValue("coopBotCount", BotCountBar2.Value.ToString());
            }

            // No need to write to the AiDefault file if we didnt have forced bot counts from the start
            if (!BotCountForced && !ForceBotCount.Checked)
                return;

            // Save Values in the AiDefault if there is changes to be made
            if (ForceBotCount.Checked)
            {
                // Create en-US culture formating for the float
                string formated = (BotDifficultyBar.Value / 100f).ToString("0.00", CultureInfo.InvariantCulture);
                AiDefaultText = AiDefaultText.Replace(AiMatches[0], "aiSettings.overrideMenuSettings 1")
                    .Replace(AiMatches[1], $"aiSettings.setMaxNBots {(BotCountBar2.Value + BotCountBar1.Value)}")
                    .Replace(AiMatches[2], $"aiSettings.setBotSkill {formated}")
                    .Replace(AiMatches[3], "aiSettings.maxBotsIncludeHumans 0");
            }
            else
            {
                // Make sure the AiDefault.ai settings are disabled
                AiDefaultText = AiDefaultText.Replace(AiMatches[0], "aiSettings.overrideMenuSettings 0");
            }

            // Save File
            File.WriteAllText(Path.Combine(MainForm.SelectedMod.RootPath, "ai", "AiDefault.ai"), AiDefaultText);
        }

        /// <summary>
        /// Event fired when the cancel button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Event fired when the user wants to save his settings
        /// </summary>
        private void SaveButton_Click(object sender, EventArgs e)
        {
            // General
            Settings.SetValue("serverName", ServerNameBox.Text);
            Settings.SetValue("password", ServerPasswordBox.Text);
            Settings.SetValue("serverIP", ServerIpBox.Text);
            Settings.SetValue("serverPort", ServerPortBox.Value.ToString());
            Settings.SetValue("gameSpyPort", GamespyPortBox.Value.ToString());
            Settings.SetValue("welcomeMessage", ServerWelcomeBox.Text);
            Settings.SetValue("sponsorText", ServerWelcomeBox.Text);
            Settings.SetValue("autoBalanceTeam", (AutoBalanceBox.Checked) ? "1" : "0");
            Settings.SetValue("punkBuster", (EnablePunkBuster.Checked) ? "1" : "0");
            Settings.SetValue("internet", (EnablePublicServerBox.Checked) ? "1" : "0");
            Settings.SetValue("roundsPerMap", RoundsPerMapBox.Value.ToString());
            Settings.SetValue("numPlayersNeededToStart", PlayersToStartSlider.Value.ToString());
            Settings.SetValue("maxPlayers", MaxPlayersBar.Value.ToString());
            Settings.SetValue("ticketRatio", TicketRatioBar.Value.ToString());
            Settings.SetValue("teamRatioPercent", TeamRatioBar.Value.ToString());
            Settings.SetValue("scoreLimit", ScoreLimitBar.Value.ToString());

            // Time limits
            Settings.SetValue("timeLimit", TimeLimitBar.Value.ToString());
            Settings.SetValue("spawnTime", SpawnTimeBar.Value.ToString());
            Settings.SetValue("manDownTime", ManDownBar.Value.ToString());
            Settings.SetValue("startDelay", StartDelayBar.Value.ToString());
            Settings.SetValue("endDelay", EndDelayBar.Value.ToString());
            Settings.SetValue("endOfRoundDelay", EORBar.Value.ToString());
            Settings.SetValue("notEnoughPlayersRestartDelay", NotEnoughPlayersBar.Value.ToString());
            Settings.SetValue("timeBeforeRestartMap", TimeB4RestartMapBar.Value.ToString());

            // Friendly Fire
            Settings.SetValue("tkPunishEnabled", (PunishTeamKillsBox.Checked) ? "1" : "0");
            Settings.SetValue("friendlyFireWithMines", (FriendlyFireBox.Checked) ? "1" : "0");
            Settings.SetValue("tkPunishByDefault", (PunishDefaultBox.Checked) ? "1" : "0");
            Settings.SetValue("tkNumPunishToKick", TksBeforeKickBox.Value.ToString());
            Settings.SetValue("soldierFriendlyFire", SoldierFFBar.Value.ToString());
            Settings.SetValue("soldierSplashFriendlyFire", SoldierSplashFFBar.Value.ToString());
            Settings.SetValue("vehicleFriendlyFire", VehicleFFBar.Value.ToString());
            Settings.SetValue("vehicleSplashFriendlyFire", VehicleSplashFFBar.Value.ToString());

            // Voip
            Settings.SetValue("voipEnabled", (EnableVoip.Checked) ? "1" : "0");
            Settings.SetValue("voipServerRemote", (EnableRemoteVoip.Checked) ? "1" : "0");
            Settings.SetValue("voipBFClientPort", VoipBF2ClientPort.Value.ToString());
            Settings.SetValue("voipBFServerPort", VoipBF2ServerPort.Value.ToString());
            Settings.SetValue("voipServerPort", VoipServerPort.Value.ToString());
            Settings.SetValue("voipQuality", VoipQualityBar.Value.ToString());
            Settings.SetValue("voipServerRemoteIP", RemoteVoipIpBox.Text);
            Settings.SetValue("voipSharedPassword", VoipPasswordBox.Text);

            // Voting
            Settings.SetValue("votingEnabled", (EnableVotingBox.Checked) ? "1" : "0");
            Settings.SetValue("teamVoteOnly", (EnableTeamVotingBox.Checked) ? "1" : "0");
            Settings.SetValue("voteTime", VoteTimeBar.Value.ToString());
            Settings.SetValue("minPlayersForVoting", PlayersVotingBar.Value.ToString());

            // Demo & Urls
            Settings.SetValue("autoRecord", (EnableAutoRecord.Checked) ? "1" : "0");
            Settings.SetValue("demoQuality", DemoQualityBar.Value.ToString());
            Settings.SetValue("demoIndexURL", DemoIndexUrlBox.Text);
            Settings.SetValue("demoDownloadURL", DemoDownloadBox.Text);
            Settings.SetValue("autoDemoHook", DemoHookBox.Text);
            Settings.SetValue("communityLogoURL", CLogoUrlBox.Text);
            Settings.SetValue("sponsorLogoURL", SLogoUrlBox.Text);

            // Misc
            Settings.SetValue("allowNATNegotiation", (AllowNATNagotiation.Checked) ? "1" : "0");
            Settings.SetValue("allowFreeCam", (AllowFreeCam.Checked) ? "1" : "0");
            Settings.SetValue("allowNoseCam", (AllowNoseCam.Checked) ? "1" : "0");
            Settings.SetValue("allowExternalViews", (AllowExtViews.Checked) ? "1" : "0");
            Settings.SetValue("hitIndicator", (HitIndicatorEnabled.Checked) ? "1" : "0");
            Settings.SetValue("radioSpamInterval", RadioSpamIntBox.Value.ToString());
            Settings.SetValue("radioMaxSpamFlagCount", RadioMaxSpamBox.Value.ToString());
            Settings.SetValue("radioBlockedDurationTime", RadioBlockTimeBar.Value.ToString());

            // Save to the file
            try
            {
                // Bot Settings
                SetBotSettings();
                Settings.Save();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Server Settings File Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region Events

        private void ForceBotCount_CheckedChanged(object sender, EventArgs e)
        {
            BotCountBar1.Enabled = ForceBotCount.Checked;
            BotCountBar2.Enabled = ForceBotCount.Checked;
            BotCountBar.Enabled = !ForceBotCount.Checked;
            BotRatioBar.Enabled = !ForceBotCount.Checked;
        }

        private void InfoLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (BotCountInfoForm f = new BotCountInfoForm())
            {
                f.ShowDialog();
            }
        }

        private void PlayersToStartSlider_ValueChanged(object sender, EventArgs e)
        {
            PlayersToStartValueBox.Text = PlayersToStartSlider.Value.ToString();
        }

        private void VoipQualityBar_ValueChanged(object sender, EventArgs e)
        {
            VoipQualityBox.Text = VoipQualityBar.Value.ToString();
        }

        private void VoteTimeBar_ValueChanged(object sender, EventArgs e)
        {
            VoteTimeBox.Text = VoteTimeBar.Value.ToString();
        }

        private void PlayersVotingBar_ValueChanged(object sender, EventArgs e)
        {
            PlayersVotingBox.Text = PlayersVotingBar.Value.ToString();
        }

        private void TimeLimitBar_ValueChanged(object sender, EventArgs e)
        {
            TimeLimitBox.Text = TimeLimitBar.Value.ToString();
        }

        private void TicketRatioBar_ValueChanged(object sender, EventArgs e)
        {
            TicketRatioBox.Text = TicketRatioBar.Value.ToString() + "%";
        }

        private void ScoreLimitBar_ValueChanged(object sender, EventArgs e)
        {
            ScoreLimitBox.Text = ScoreLimitBar.Value.ToString();
        }

        private void SpawnTimeBar_ValueChanged(object sender, EventArgs e)
        {
            SpawnTimeBox.Text = SpawnTimeBar.Value.ToString();
        }

        private void ManDownBar_ValueChanged(object sender, EventArgs e)
        {
            ManDownBox.Text = ManDownBar.Value.ToString();
        }

        private void TeamRatioBar_ValueChanged(object sender, EventArgs e)
        {
            TeamRatioBox.Text = TeamRatioBar.Value.ToString() + "%";
        }

        private void SoldierFFBar_ValueChanged(object sender, EventArgs e)
        {
            SoldierFFBox.Text = SoldierFFBar.Value.ToString() + "%";
        }

        private void SoldierSplashFFBar_ValueChanged(object sender, EventArgs e)
        {
            SoldierSplashFFBox.Text = SoldierSplashFFBar.Value.ToString() + "%";
        }

        private void VehicleFFBar_ValueChanged(object sender, EventArgs e)
        {
            VehicleFFBox.Text = VehicleFFBar.Value.ToString() + "%";
        }

        private void VehicleSplashFFBar_ValueChanged(object sender, EventArgs e)
        {
            VehicleSplashFFBox.Text = VehicleSplashFFBar.Value.ToString() + "%";
        }

        private void BotRatioBar_ValueChanged(object sender, EventArgs e)
        {
            BotRatioBox.Text = BotRatioBar.Value.ToString() + "%";
        }

        private void BotCountBar_ValueChanged(object sender, EventArgs e)
        {
            BotCountBox.Text = BotCountBar.Value.ToString();
        }

        private void BotDifficultyBar_ValueChanged(object sender, EventArgs e)
        {
            BotDifficultyBox.Text = BotDifficultyBar.Value.ToString();
        }

        private void MaxPlayersBar_ValueChanged(object sender, EventArgs e)
        {
            MaxPlayersBox.Text = MaxPlayersBar.Value.ToString();
        }

        private void StartDelayBar_ValueChanged(object sender, EventArgs e)
        {
            StartDelayBox.Text = StartDelayBar.Value.ToString();
        }

        private void EndDelayBar_ValueChanged(object sender, EventArgs e)
        {
            EndDelayBox.Text = EndDelayBar.Value.ToString();
        }

        private void EORBar_ValueChanged(object sender, EventArgs e)
        {
            EORBox.Text = EORBar.Value.ToString();
        }

        private void NotEnoughPlayersBar_ValueChanged(object sender, EventArgs e)
        {
            NotEnoughPlayersBox.Text = NotEnoughPlayersBar.Value.ToString();
        }

        private void TimeB4RestartMapBar_ValueChanged(object sender, EventArgs e)
        {
            TimeB4RestartMapBox.Text = TimeB4RestartMapBar.Value.ToString();
        }

        private void PunishTeamKillsBox_CheckedChanged(object sender, EventArgs e)
        {
            PunishDefaultBox.Enabled = PunishTeamKillsBox.Checked;
            TksBeforeKickBox.Enabled = PunishTeamKillsBox.Checked;
        }

        private void EnableRemoteVoip_CheckedChanged(object sender, EventArgs e)
        {
            if (EnableRemoteVoip.Checked)
            {
                if (!EnableVoip.Checked)
                {
                    EnableVoip.Checked = true;
                    EnableRemoteVoip.Checked = true;
                }

                RemoteVoipIpBox.Enabled = true;
                VoipPasswordBox.Enabled = true;
                VoipServerPort.Enabled = true;
                VoipQualityBar.Enabled = false;
            }
            else
            {
                RemoteVoipIpBox.Enabled = false;
                VoipPasswordBox.Enabled = false;
                VoipServerPort.Enabled = false;
                VoipQualityBar.Enabled = true;
            }
        }

        private void EnableVoip_CheckedChanged(object sender, EventArgs e)
        {
            if (!EnableVoip.Checked && EnableRemoteVoip.Checked)
                EnableRemoteVoip.Checked = false;
        }

        private void DemoQualityBar_ValueChanged(object sender, EventArgs e)
        {
            DemoQualityBox.Text = DemoQualityBar.Value.ToString();
        }

        private void RadioBlockTimeBar_ValueChanged(object sender, EventArgs e)
        {
            RadioBlockTimeBox.Text = RadioBlockTimeBar.Value.ToString();
        }

        private void BotCountBar1_ValueChanged(object sender, EventArgs e)
        {
            BotCountBox1.Text = BotCountBar1.Value.ToString();
        }

        private void BotCountBar2_ValueChanged(object sender, EventArgs e)
        {
            BotCountBox2.Text = BotCountBar2.Value.ToString();
        }

        #endregion
    }
}
