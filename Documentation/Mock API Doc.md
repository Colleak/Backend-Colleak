## Summary
These are the endpoints of our Mock API

## Example Usage


## Code Analysis
### Inputs
- `data`: The JSON data received in the request.
- `required_fields`: A list of fields that are required in the input data.
___
### Flow
1. The code defines a Flask application and sets up logging.
2. There is a function `validate_input` that checks if the required fields are present in the input data.
3. There are several routes defined for different actions like sending messages, pings, calls, and checking availability.
4. Each route validates the input data using the `validate_input` function and returns an error response if any required fields are missing.
5. The routes perform the required actions based on the input data and log the relevant information.

___

The API has the following routes:
- GET /: Returns a welcome message.
- POST /`send_message`: Sends a message from one user to another.
- POST /`send_ping`: Sends a ping from one user to another.
- POST /`send_call`: Initiates a call from one user to another.
- POST /`atm_available`: Checks if a user is available at the current hour.
- POST /`available`: Checks if a user is available at a specific time.
- POST /`on_location`: Returns the current location status of a user.
- POST /`set_location`: Sets the location status of a user.
- POST /`disturb`: Handles the disturbance status of a user.

```python
# Send a message
POST /send_message
{
    "sender_id": "653fad3009ae93a5292195d4",
    "receiver_id": "653fb6cb09ae93a5292195d6",
    "message": "Hello, When do you have time to meet?",
    "sender_name": "Art Nooijen",
    "receiver_name": "jelle Manders"
}

# Send a ping
POST /send_ping
{
    "sender_id": "653fad3009ae93a5292195d4",
    "receiver_id": "653fb6cb09ae93a5292195d6",
    "sender_name": "Art Nooijen",
    "receiver_name": "jelle Manders"
}

# Initiate a call
POST /send_call
{
    "sender_id": "653fad3009ae93a5292195d4",
    "receiver_id": "653fb6cb09ae93a5292195d6",
    "sender_name": "Art Nooijen",
    "receiver_name": "jelle Manders"
}

# Check if a person is available at the current hour
POST /atm_available
{
    "receiver_id": "653fb6cb09ae93a5292195d6",
    "receiver_name": "jelle Manders"
}

# Check if a person is available at a specific hour
POST /available
{
    "receiver_id": "653fb6cb09ae93a5292195d6",
    "receiver_name": "jelle Manders",
    "request_time": "11"
}

# Set disturb status for a person
POST /disturb
{
    "receiver_id": "653fb6cb09ae93a5292195d6",
    "receiver_name": "jelle Manders",
    "disturb": "true"
}

# Check if a person is on location or working from home
POST /is_on_location
{
    "receiver_id": "653fb6cb09ae93a5292195d6",
    "receiver_name": "jelle Manders",
    "is_on_location": "true"
}
```

Each route expects JSON data as input and returns a JSON response with a status field indicating the success or error status, and a message field providing additional information. Some routes also include additional fields such as Time or AvailableArray.

We also validate the input data to ensure that all required fields are present. If any required fields are missing, we return an error response.

To run the API, execute the script and the API will be accessible at artofghost.pythonanywhere.com.

```python
from flask import Flask, request, jsonify
import logging
import datetime

app = Flask(__name__)
logging.basicConfig(level=logging.INFO)

# mock body for the post request

# {
#     "sender_name": "Art Nooijen",
#     "receiver_name": "jelle Manders",
#     "sender_id": "653fad3009ae93a5292195d4",
#     "receiver_id": "653fb6cb09ae93a5292195d6",
#     "message": "Hello, When do you have time to meet?",
#     "request_time": "11",
#     "disturb": "true"
# }

def validate_input(data, required_fields):
    missing_fields = [field for field in required_fields if field not in data]
    if missing_fields:
        return False, f"Missing fields: {', '.join(missing_fields)}"
    return True, ""

@app.route('/', methods=['GET'])
def Hello():
    test = "Welcome to Colleaks Mock API"
    return test

@app.route('/send_message', methods=['POST'])
def send_message():
    data = request.json
    valid, error_message = validate_input(data, ['sender_id', 'receiver_id', 'message', 'sender_name', 'receiver_name'])
    if not valid:
        return jsonify({"status": "error", "message": error_message}), 400

    sender_id = data['sender_id']
    receiver_id = data['receiver_id']
    message = data['message']
    sender_name = data['sender_name']
    receiver_name = data['receiver_name']

    logging.info(f"Message from {sender_name}({sender_id}) to {receiver_name}({receiver_id}): {message}")
    return jsonify({"status": "success", "message": message}),200

@app.route('/send_ping', methods=['POST'])
def send_ping():
    data = request.json
    valid, error_message = validate_input(data, ['sender_id', 'receiver_id', 'sender_name', 'receiver_name'])
    if not valid:
        return jsonify({"status": "error", "message": error_message}), 400

    sender_id = data['sender_id']
    receiver_id = data['receiver_id']
    message = 'is looking for you!'
    sender_name = data['sender_name']
    receiver_name = data['receiver_name']

    logging.info(f"{sender_name}({sender_id}) {message} Send to: {receiver_name}({receiver_id})")
    return jsonify({"status": "success", "message": "User pinged successfully"})

@app.route('/send_call', methods=['POST'])
def send_call():
    data = request.json
    # Validate input
    valid, error_message = validate_input(data, ['sender_id', 'receiver_id', 'sender_name', 'receiver_name'])
    if not valid:
        return jsonify({"status": "error", "message": error_message}), 400

    caller_id = data['sender_id']
    receiver_id = data['receiver_id']
    caller_name = data['sender_name']
    receiver_name = data['receiver_name']

    logging.info(f"Call from {caller_name}({caller_id}) to {receiver_name}({receiver_id})")
    return jsonify({"status": "success", "message": "Call initiated"}), 200

Not_Available_Array = [20,11,12,13,16,18]
date =  datetime.datetime.now()
current_hour = date.hour

@app.route('/atm_available', methods=['POST'])
def is_person_atm_available():
    data = request.json
    valid, error_message = validate_input(data, ['receiver_id', 'receiver_name'])
    if not valid:
        return jsonify({"status": "error", "message": error_message}), 400

    receiver_id = data['receiver_id']
    request_time = int(current_hour) + 1
    receiver_name = data['receiver_name']

    is_available = request_time not in Not_Available_Array

    if is_available:
        logging.info(f"{receiver_name}({receiver_id}) is available. Send to: {receiver_name}({receiver_id}) at {request_time}")
        return jsonify({"status": "success", "message": "User is currently available", "Time": request_time})
    else:
        logging.info(f"{receiver_name}({receiver_id}) is not available at {request_time}")
        return jsonify({"status": "success", "message": "User is currently not available"})

@app.route('/available', methods=['POST'])
def is_person_available():
    data = request.json
    valid, error_message = validate_input(data, ['receiver_id', 'receiver_name', 'request_time'])
    if not valid:
        return jsonify({"status": "error", "message": error_message}), 400

    receiver_id = data['receiver_id']
    request_time = int(data['request_time'])
    receiver_name = data['receiver_name']

    is_available = request_time not in Not_Available_Array

    if is_available:
        logging.info(f"{receiver_name}({receiver_id}) is available. Send to: {receiver_name}({receiver_id}) at {request_time}")
        return jsonify({"status": "success", "message": "User is available" , "AvailableArray": Not_Available_Array})
    else:
        logging.info(f"{receiver_name}({receiver_id}) is not available at {request_time}")
        return jsonify({"status": "success", "message": "User is not available","AvailableArray": Not_Available_Array})

on_location = True

@app.route('/on_location', methods=['POST'])
def on_location_get():
    data = request.json
    valid, error_message = validate_input(data, ['sender_id'])
    if not valid:
        return jsonify({"status": "error", "message": error_message}), 400

    sender_id = data['sender_id']

    return jsonify({"status": "success", "message": on_location, "user_id": sender_id})

@app.route('/set_location', methods=['POST'])
def set_location():
    global on_location
    data = request.json
    valid, error_message = validate_input(data, ['sender_id', 'sender_name'])
    if not valid:
        return jsonify({"status": "error", "message": error_message}), 400

    receiver_id = data['sender_id']
    receiver_name = data['sender_name']

    if on_location:
        on_location = False
        logging.info(f"{receiver_name}({receiver_id}) is not on location")
        return jsonify({"status": "success", "message": "You are not on location"})

    else :
        on_location = True
        logging.info(f"{receiver_name}({receiver_id}) is on location")
        return jsonify({"status": "success", "message": "You are on location"})

@app.route('/disturb', methods=['POST'])
def disturb():
    data = request.json
    valid, error_message = validate_input(data, ['receiver_id', 'receiver_name', 'disturb'])
    if not valid:
        return jsonify({"status": "error", "message": error_message}), 400

    receiver_id = data['receiver_id']
    receiver_name = data['receiver_name']
    disturb = data['disturb'].lower() == 'true'

    if disturb:
        logging.info(f"{receiver_name}({receiver_id}) can be contacted")
        return jsonify({"status": "success", "message": "You are able to be contacted"})
    else:
        logging.info(f"{receiver_name}({receiver_id}) cannot be contacted")
        return jsonify({"status": "success", "message": "Your location is no longer shared"})


if __name__ == '__main__':
    app.run(debug=True,port=8001)
```
