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

Not_Available_Array = [11, 12, 13, 18, 19]
date =  datetime.datetime.now()
current_hour = date.hour

@app.route('/atm_available', methods=['POST'])
def is_person_atm_available():
    data = request.json
    valid, error_message = validate_input(data, ['receiver_id', 'receiver_name'])
    if not valid:
        return jsonify({"status": "error", "message": error_message}), 400

    receiver_id = data['receiver_id']
    request_time = int(current_hour)
    receiver_name = data['receiver_name']

    is_available = request_time not in Not_Available_Array

    if is_available:
        logging.info(f"{receiver_name}({receiver_id}) is available. Send to: {receiver_name}({receiver_id}) at {request_time}")
        return jsonify({"status": "success", "message": "User is currently available"})
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
        return jsonify({"status": "success", "message": "User is available"})
    else:
        logging.info(f"{receiver_name}({receiver_id}) is not available at {request_time}")
        return jsonify({"status": "success", "message": "User is not available"})

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

@app.route('/is_on_location', methods=['POST'])
def is_on_location():
    data = request.json
    valid, error_message = validate_input(data, ['receiver_id', 'receiver_name', 'is_on_location'])
    if not valid:
        return jsonify({"status": "error", "message": error_message}), 400

    receiver_id = data['receiver_id']
    receiver_name = data['receiver_name']

    if isinstance(data['is_on_location'], bool):
        is_on_location = data['is_on_location']
    else:
        is_on_location = data['is_on_location'].lower() == 'true'

    if is_on_location:
        logging.info(f"{receiver_name}({receiver_id}) is on location")
        return jsonify({"status": "success", "message": "Is on location", "is_on_location": "True"})
    else:
        logging.info(f"{receiver_name}({receiver_id}) is working from home")
        return jsonify({"status": "success", "message": "Is working from home","is_on_location": "False"})

if __name__ == '__main__':
    app.run(debug=True,port=8001)
