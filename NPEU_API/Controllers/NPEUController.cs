using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Net;
using System.Threading.Tasks;

namespace NPEU_API.Controllers
{
    [ApiController]
   
    public class NPEUController : ControllerBase
    {

        private ParticipantsDetialsInsert participantsDetialsInsert;
        private Apiresponse response;

        [Route("record")]
        [HttpPost]
        public string RecordParticipantDetails(ParticpantDetails partDetails)

        {
            participantsDetialsInsert = new ParticipantsDetialsInsert();
            response = new Apiresponse();

            // validation of request
            if (!validateParticipantDetails(partDetails))
            {
                response.Httpcode = (int)HttpStatusCode.BadRequest;
                Response.StatusCode = response.Httpcode;
                return JsonSerializer.Serialize(response);
            }

            // call database to store
            Boolean resp = participantsDetialsInsert.insertRecord(partDetails);

            if (resp)
            {
                response.Httpcode = (int)HttpStatusCode.Created;
                response.Message = "Created Participant Record";
                Response.StatusCode = response.Httpcode;
            }
            else
            {
                response.Httpcode = (int)HttpStatusCode.BadRequest;
                response.Message = "Unable to Create Participant Record";
                Response.StatusCode = response.Httpcode;
            }

            return JsonSerializer.Serialize(response);

        }


        private Boolean validateParticipantDetails(ParticpantDetails partDetails)
        {
            if (partDetails.participantId <= 0)
            {

                response.Message = "Participant Id should be > 0";

                return false;
            }

            if (partDetails.dateTime == null)
            {

                response.Message = "Incorrect Date Time";
                return false;
            }

            if (partDetails.dosage <= 0)
            {

                response.Message = "Incorrect Dosage";
                return false;
            }

            if (partDetails.weight <= 0)
            {

                response.Message = "Incorrect Weight";
                return false;
            }
           
           

            return true;
        }
    }
}
