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

        [TestInitialize]
        public void StartApp()
        {
            var appPath = Directory.GetCurrentDirectory();
            appPath = appPath.Remove(appPath.LastIndexOf("TestResults"))
                      + @"SistSeguridad\bin\Debug\SistSeguridad.exe";

            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = appPath;
            myProcess = Process.Start(start);
            System.Threading.Thread.Sleep(2000);
        }

        [TestCleanup]
        public void CloseApp()
        {
            myProcess.CloseMainWindow();
            System.Threading.Thread.Sleep(2000);
        }

        [TestMethod]
        public void PruebaCambioClave1_VerificacionFuncionalidadBasica()
        {
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
            // Cambio de Clave
            this.UIMap.Rivera_CambioClave_DigitarClavePorDefecto();

            // Fallar confirmacion de clave
            this.UIMap.Rivera_DigitarClaveModificada();
            this.UIMap.Rivera_DigitarClavePorDefecto();

            // Verificar mensaje de error
            this.UIMap.Rivera_AssertLcdErrorOn();

            // To generate code for this test, select "Generate Code for Coded UI Test" from the shortcut menu and select one of the menu items.
        }

        [TestMethod]
        public void PruebaCambioClave4_ArmadoDuranteCambioDeClave()
        {
            // Cambio de Clave
            this.UIMap.Rivera_CambioClave_DigitarClavePorDefecto();

            // Tratar de armar sistema durante el cambio de clave
            this.UIMap.Rivera_DigitarArmadoZona0ClavePorDefecto();

            // Verificar mensaje de error
            this.UIMap.Rivera_AssertLcdErrorOn();

            // To generate code for this test, select "Generate Code for Coded UI Test" from the shortcut menu and select one of the menu items.
        }

        [TestMethod]
        public void PruebaCambioClave5_SalirDeCambioDeClave()
        {
            // Cambio de Clave
            this.UIMap.Rivera_CambioClave_DigitarClavePorDefecto();

            // Salir del cambio de clave oprimiendo "Esc"
            this.UIMap.Rivera_CleanLcd();

            // Armar sistema con clave por defecto
            this.UIMap.Rivera_DigitarArmadoZona0ClavePorDefecto();

            // Verificar mensajes de error y armado
            this.UIMap.Rivera_AssertLcdErrorOff();
            this.UIMap.Rivera_AssertLcdArmadoOn();

            // To generate code for this test, select "Generate Code for Coded UI Test" from the shortcut menu and select one of the menu items.
        }

        [TestMethod]
        public void PruebaCambioClave6_SalirDeCambioDeClave2()
        {
            // Cambio de Clave
            this.UIMap.Rivera_CambioClave_DigitarClavePorDefecto();
            this.UIMap.Rivera_DigitarClaveModificada();

            // Salir de la confirmacion de clave oprimiendo "Esc"
            this.UIMap.Rivera_CleanLcd();

            // Armar sistema con clave por defecto
            this.UIMap.Rivera_DigitarArmadoZona0ClavePorDefecto();

            // Verificar mensajes de error y armado
            this.UIMap.Rivera_AssertLcdErrorOff();
            this.UIMap.Rivera_AssertLcdArmadoOn();

            // To generate code for this test, select "Generate Code for Coded UI Test" from the shortcut menu and select one of the menu items.
        }

        [TestMethod]
        public void PruebaCambioClave7_ActivarAlarmDuranteCambioDeClave()
        {
            // Configurar sensores y zonas
            this.UIMap.Rivera_AbrirVentanaSimulador();
            this.UIMap.Rivera_AsignarSensor1Zona0();

            // Armar sistema en Zona0
            this.UIMap.Rivera_DigitarArmadoZona0ClavePorDefecto();

            // Verificar indicadores en el LCD de alarma y armado
            this.UIMap.Rivera_AssertLcdArmadoOn();
            this.UIMap.Rivera_AssertLcdAlarmaOff();

            // Cambio de Clave
            this.UIMap.Rivera_CambioClave_DigitarClavePorDefecto();
            this.UIMap.Rivera_DigitarClaveModificada();
            
            // Activar Alarma
            this.UIMap.Rivera_ActivarSensor1();
            this.UIMap.Rivera_DesactivarSensor1();
            this.UIMap.Rivera_CerrarVentanaSimulador();         

            // Desarmar alarma (se tuvo que haber cancelado el cambio de clave?)
            this.UIMap.Rivera_DigitarClavePorDefecto();

            // Verificar indicadores en el LCD de alarma y armado
            this.UIMap.Rivera_AssertLcdArmadoOff();
            this.UIMap.Rivera_AssertLcdAlarmaOff();

            // To generate code for this test, select "Generate Code for Coded UI Test" from the shortcut menu and select one of the menu items.
        }

        [TestMethod]
        public void PruebaCambioClave8_ActivarAlarmDuranteCambioDeClave2()
        {
            // Configurar sensores y zonas
            this.UIMap.Rivera_AbrirVentanaSimulador();
            this.UIMap.Rivera_AsignarSensor1Zona0();

            // Armar sistema en Zona0
            this.UIMap.Rivera_DigitarArmadoZona0ClavePorDefecto();

            // Verificar indicadores en el LCD de alarma y armado
            this.UIMap.Rivera_AssertLcdArmadoOn();
            this.UIMap.Rivera_AssertLcdAlarmaOff();

            // Cambio de Clave
            this.UIMap.Rivera_CambioClave_DigitarClavePorDefecto();
            this.UIMap.Rivera_DigitarClaveModificada();

            // Activar Alarma
            this.UIMap.Rivera_ActivarSensor1();
            this.UIMap.Rivera_DesactivarSensor1();
            this.UIMap.Rivera_CerrarVentanaSimulador();

            // Continuar con el cambio de clave (debe de fallar porque hay una alarma...)
            this.UIMap.Rivera_DigitarClaveModificada();

            // Desarmar sistema con la "supuesta" nueva clave
            this.UIMap.Rivera_DigitarClaveModificada();

            // Verificar indicadores en el LCD de alarma y armado
            this.UIMap.Rivera_AssertLcdArmadoOn();
            this.UIMap.Rivera_AssertLcdAlarmaOn();

            // To generate code for this test, select "Generate Code for Coded UI Test" from the shortcut menu and select one of the menu items.
        }

        [TestMethod]
        public void PruebaCambioClave9_CambioDeClaveDuranteAlarma()
        {
            // Configurar sensores y zonas
            this.UIMap.Rivera_AbrirVentanaSimulador();
            this.UIMap.Rivera_AsignarSensor1Zona0();

            // Armar sistema en Zona0
            this.UIMap.Rivera_DigitarArmadoZona0ClavePorDefecto();

            // Verificar indicadores en el LCD de alarma y armado
            this.UIMap.Rivera_AssertLcdArmadoOn();
            this.UIMap.Rivera_AssertLcdAlarmaOff();

            // Activar Alarma
            this.UIMap.Rivera_ActivarSensor1();
            this.UIMap.Rivera_DesactivarSensor1();
            this.UIMap.Rivera_CerrarVentanaSimulador();

            // Cambio de Clave
            this.UIMap.Rivera_CambioClave_DigitarClavePorDefecto();
            this.UIMap.Rivera_DigitarClaveModificada();
            this.UIMap.Rivera_DigitarClaveModificada();

            // Desarmar sistema con la "supuesta" nueva clave
            this.UIMap.Rivera_DigitarClaveModificada();

            // Verificar indicadores en el LCD de alarma y armado
            this.UIMap.Rivera_AssertLcdArmadoOn();
            this.UIMap.Rivera_AssertLcdAlarmaOn();

            // To generate code for this test, select "Generate Code for Coded UI Test" from the shortcut menu and select one of the menu items.
        }

        [TestMethod]
        public void PruebaCambioClave10_ClaveIncorrecta3Veces()
        {
            // Verificar que no hay alarma
            this.UIMap.Rivera_AssertLcdAlarmaOff();

            // Fallar la clave 3 veces
            // Cambio de Clave
            this.UIMap.Rivera_CambioClave_DigitarClavePorDefecto();
            // Fallo 1
            this.UIMap.Rivera_DigitarClaveModificada();
            this.UIMap.Rivera_DigitarClavePorDefecto();
            // Fallo 2
            this.UIMap.Rivera_DigitarClaveModificada();
            this.UIMap.Rivera_DigitarClavePorDefecto();
            // Fallo 3
            this.UIMap.Rivera_DigitarClaveModificada();
            this.UIMap.Rivera_DigitarClavePorDefecto();
            // Fallo 4
            this.UIMap.Rivera_DigitarClaveModificada();
            this.UIMap.Rivera_DigitarClavePorDefecto();

            // Verificar que se activo alarma
            this.UIMap.Rivera_AssertLcdAlarmaOn();

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
