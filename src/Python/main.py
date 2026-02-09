from http.server import HTTPServer, BaseHTTPRequestHandler
import json
from datetime import datetime

class SimpleAPIHandler(BaseHTTPRequestHandler):
    def do_GET(self):
        """Handle GET requests"""
        if self.path == '/api/hello' or self.path == '/':
            self.send_response(200)
            self.send_header('Content-type', 'application/json')
            self.end_headers()
            
            response = {
                'message': 'Hello world from Python API',
                'timestamp': datetime.utcnow().isoformat()
            }
            
            self.wfile.write(json.dumps(response).encode())
        else:
            self.send_response(404)
            self.send_header('Content-type', 'application/json')
            self.end_headers()
            
            response = {'error': 'Not Found'}
            self.wfile.write(json.dumps(response).encode())
    
    def log_message(self, format, *args):
        """Override to customize logging"""
        print(f"[{datetime.now().strftime('%Y-%m-%d %H:%M:%S')}] {format % args}")

def run(server_class=HTTPServer, handler_class=SimpleAPIHandler, port=8001):
    server_address = ('', port)
    httpd = server_class(server_address, handler_class)
    print(f'Starting Python API server on port {port}...')
    print(f'Access the API at: http://localhost:{port}/api/hello')
    httpd.serve_forever()

if __name__ == '__main__':
    run()
