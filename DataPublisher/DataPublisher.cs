using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataParser;

namespace DataPublisher
{
    public class DataPublisher
    {
        public static void Main(string[] args)
        {

            DDS.DomainParticipant participant =
                DDS.DomainParticipantFactory.get_instance().create_participant(
                        0,
                        DDS.DomainParticipantFactory.PARTICIPANT_QOS_DEFAULT,
                        null, /* Listener */
                        DDS.StatusMask.STATUS_MASK_NONE);
            if (participant == null)
            {
                Console.Error.WriteLine("Unable to create DDS domain participant");
                return;
            }

            // Create the topic "Hello World" for the String type
            DDS.Topic topic = participant.create_topic(
                        "DataStream",
                        DDS.StringTypeSupport.get_type_name(),
                        DDS.DomainParticipant.TOPIC_QOS_DEFAULT,
                        null, /* Listener */
                        DDS.StatusMask.STATUS_MASK_NONE);
            if (topic == null)
            {
                Console.Error.WriteLine("Unable to create topic");
                return;
            }

            // Create the data writer using the default publisher
            DDS.StringDataWriter ddsWriter = (DDS.StringDataWriter)participant.create_datawriter(
                            topic,
                            DDS.Publisher.DATAWRITER_QOS_DEFAULT,
                            null, /* Listener */
                            DDS.StatusMask.STATUS_MASK_NONE);
            if (ddsWriter == null)
            {
                Console.Error.WriteLine("Unable to create DDS data writer");
                return;
            }
            
            var xml = new DataParser<XmlData>(new XmlData());
            
            Console.WriteLine("Ready to write data.");
            Console.WriteLine("When the subscriber is ready, you can start writing.");
            Console.Write("Press CTRL+C to terminate or enter an empty line to do a clean shutdown.\n\n");

            for (; ; )
            {
                //Console.Write("Please type a message> ");
                //string toWrite = Console.In.ReadLine();
                xml.Parse();
                var toWrite = xml.TransformToJson();

                Console.Write(toWrite);
                try
                {
                    ddsWriter.write(toWrite, ref DDS.InstanceHandle_t.HANDLE_NIL);
                }
                catch (DDS.Retcode_Error e)
                {
                    Console.Error.WriteLine("Write error: " + e.Message);
                    break;
                }
                if (toWrite == "")
                {
                    break;
                }
            }
            Console.WriteLine("Shutting down...");
            participant.delete_contained_entities();
            DDS.DomainParticipantFactory.get_instance().delete_participant(ref participant);
        }
    }
}
