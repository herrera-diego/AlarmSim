using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Input;
using System.Windows.Forms;
using System.Drawing;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UITest.Extension;
using Keyboard = Microsoft.VisualStudio.TestTools.UITesting.Keyboard;
using System.Diagnostics;
using System.IO;

namespace UnitTest
{
    /// <summary>
    /// Summary description for CodedUITest1
    /// </summary>
    [CodedUITest]
    public class CodedUITest1
    {
        internal static Process myProcess;

        public CodedUITest1()
        {
        }

        [ClassInitialize]
        public static void StartApp(TestContext testContext)
        {
            var appPath = Directory.GetCurrentDirectory();
            appPath = appPath.Remove(appPath.LastIndexOf("TestResults"))
                      + @"SistSeguridad\bin\Debug\SistSeguridad.exe";

            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = appPath;
            myProcess = Process.Start(start);
            System.Threading.Thread.Sleep(2000);
        }

        [ClassCleanup]
        public static void CloseApp()
        {
            myProcess.Close();
        }

        [TestMethod]
        public void PruebaCambioClave1_VerificacionFuncionalidadBasica()
        {
            // Verificar estado inicial
            this.UIMap.Rivera_CleanLcd();
            this.UIMap.Rivera_AssertLcdArmadoOff();
            this.UIMap.Rivera_AssertLcdAlarmaOff();

            // Configurar sensores y zonas
            this.UIMap.Rivera_AbrirVentanaSimulador();
            this.UIMap.Rivera_AsignarSensor1Zona0();

            // Armar sistema en Zona0
            this.UIMap.Rivera_DigitarArmadoZona0ClavePorDefecto();

            // Activar Alarma
            this.UIMap.Rivera_ActivarSensor1();
            this.UIMap.Rivera_DesactivarSensor1();

            // Volver a panel de control
            this.UIMap.Rivera_CerrarVentanaSimulador();
            this.UIMap.Rivera_CleanLcd();

            // Verificar indicadores en el LCD de alarma y armado
            this.UIMap.Rivera_AssertLcdArmadoOn();
            this.UIMap.Rivera_AssertLcdAlarmaOn();

            // Desarmar alarma
            this.UIMap.Rivera_DigitarClavePorDefecto();

            // Verificar indicadores en el LCD de alarma y armado
            this.UIMap.Rivera_AssertLcdArmadoOff();
            this.UIMap.Rivera_AssertLcdAlarmaOff();

            // To generate code for this test, select "Generate Code for Coded UI Test" from the shortcut menu and select one of the menu items.
        }


        [TestMethod]
        public void PruebaCambioClave2_CambioDeClave()
        {
            // Verificar estado inicial
            this.UIMap.Rivera_CleanLcd();
            this.UIMap.Rivera_AssertLcdArmadoOff();
            this.UIMap.Rivera_AssertLcdAlarmaOff();

            // Configurar sensores y zonas
            this.UIMap.Rivera_AbrirVentanaSimulador();
            this.UIMap.Rivera_AsignarSensor1Zona0();

            // Cambio de Clave
            this.UIMap.Rivera_CambioClave_DigitarClavePorDefecto();
            this.UIMap.Rivera_DigitarClaveModificada();
            this.UIMap.Rivera_DigitarClaveModificada();

            // Armar sistema en Zona0
            this.UIMap.Rivera_DigitarArmadoZona0ClaveModificada();

            // Activar Alarma
            this.UIMap.Rivera_ActivarSensor1();
            this.UIMap.Rivera_DesactivarSensor1();

            // Volver a panel de control
            this.UIMap.Rivera_CerrarVentanaSimulador();
            this.UIMap.Rivera_CleanLcd();

            // Verificar indicadores en el LCD de alarma y armado
            this.UIMap.Rivera_AssertLcdArmadoOn();
            this.UIMap.Rivera_AssertLcdAlarmaOn();

            // Desarmar alarma
            this.UIMap.Rivera_DigitarClaveModificada();

            // Verificar indicadores en el LCD de alarma y armado
            this.UIMap.Rivera_AssertLcdArmadoOff();
            this.UIMap.Rivera_AssertLcdAlarmaOff();

            // To generate code for this test, select "Generate Code for Coded UI Test" from the shortcut menu and select one of the menu items.
        }

        [TestMethod]
        public void PruebaCambioClave3_FalloCambioDeClave()
        {
            // Verificar estado inicial
            this.UIMap.Rivera_CleanLcd();
            this.UIMap.Rivera_AssertLcdArmadoOff();
            this.UIMap.Rivera_AssertLcdAlarmaOff();

            // Configurar sensores y zonas
            this.UIMap.Rivera_AbrirVentanaSimulador();
            this.UIMap.Rivera_AsignarSensor1Zona0();

            // Cambio de Clave
            this.UIMap.Rivera_CambioClave_DigitarClavePorDefecto();
            this.UIMap.Rivera_DigitarClaveModificada();
            this.UIMap.Rivera_DigitarClavePorDefecto();

            // To generate code for this test, select "Generate Code for Coded UI Test" from the shortcut menu and select one of the menu items.
        }


        #region Additional test attributes

        // You can use the following additional attributes as you write your tests:

        ////Use TestInitialize to run code before running each test 
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{        
        //    // To generate code for this test, select "Generate Code for Coded UI Test" from the shortcut menu and select one of the menu items.
        //}

        ////Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{        
        //    // To generate code for this test, select "Generate Code for Coded UI Test" from the shortcut menu and select one of the menu items.
        //}

        #endregion

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }
        private TestContext testContextInstance;

        public UIMap UIMap
        {
            get
            {
                if (this.map == null)
                {
                    this.map = new UIMap();
                }

                return this.map;
            }
        }

        private UIMap map;
    }
}
