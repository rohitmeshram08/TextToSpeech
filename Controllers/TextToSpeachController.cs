using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Speech.Synthesis;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Net;

namespace Textospeach.Controllers
{
    public class TextToSpeachController : Controller
    {
        // GET: TextToSpeach
        private static SpeechSynthesizer synthesizer = new SpeechSynthesizer();

        public ActionResult Index()
        {
            return View();
        }

        // POST: TextToSpeech/Speak
        [HttpPost]
        public async Task<ActionResult> Speak(string text)
        {
            try
            {
                if (synthesizer.State == SynthesizerState.Speaking)
                {

                }
                else if (synthesizer.State == SynthesizerState.Paused)
                {
                    await Task.Run(() => synthesizer.Resume());
                }
                else
                {
                    // Speak the provided text asynchronously
                    await Task.Run(() => synthesizer.Speak(text));
                }
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }


        [HttpPost]
        public async Task<ActionResult> Pause()
        {
            try
            {

                if (synthesizer.State == SynthesizerState.Speaking)
                {
                    await Task.Run(() => synthesizer.Pause());
                }
                else if (synthesizer.State == SynthesizerState.Paused)
                {
                    await Task.Run(() => synthesizer.Resume());
                }
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }


        [HttpPost]

        public async Task<ActionResult> Reset()
        {
            try
            {
                if (synthesizer.State == SynthesizerState.Speaking)
                {
                    await Task.Run(() => synthesizer.Pause());
                    await Task.Run(() => synthesizer.SpeakAsyncCancelAll());
                }
                if (synthesizer.State == SynthesizerState.Paused)
                {
                    await Task.Run(() => synthesizer.SpeakAsyncCancelAll());
                }
                synthesizer.Dispose();
                synthesizer = new SpeechSynthesizer();
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }


        public ActionResult About()
        {
            return View();
        }
    }
}