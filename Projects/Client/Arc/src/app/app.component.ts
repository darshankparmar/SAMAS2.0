import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'Arc';

  constructor(private router: Router) { }

  logout() {
    localStorage.removeItem('authToken');
    // Redirect to the login page or home page
    this.router.navigate(['/login']);
  }
}
