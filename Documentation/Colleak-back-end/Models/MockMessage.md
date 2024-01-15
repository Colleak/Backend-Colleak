Het MockMessage-model bevat de informatie die terugkomt van de mock Api. Dit is omdat we de mock Api data baseren op de data die van de Microsoft Graphql Api terug kunnen verwachten.

MockMessage bestaat uit:
- string?: sender_name
- string: receiver_name
- string?: sender_id
- string?: receiver_id
- string?: message
- int? request_time
- string?: disturb
- string?: is_on_location