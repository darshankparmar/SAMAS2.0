import { Component } from '@angular/core';
import { ApiService } from '../api.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  username: string = '';
  password: string = '';

  constructor(private apiService: ApiService) { }

  login() {
    this.apiService.login(this.username, this.password).subscribe(
      (response: any) => {
        // Assuming the response contains a token
        localStorage.setItem('authToken', response.token);
        // Handle successful login
      },
      (error) => {
        // Handle error
        console.error('Login failed', error);
      }
    );
  }
}
