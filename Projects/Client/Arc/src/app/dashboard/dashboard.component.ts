import { Component, OnInit } from '@angular/core';
import { ApiService } from '../api.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html'
})
export class DashboardComponent implements OnInit {
  homeData: string[] = [];
  error!: string;
  constructor(private apiService: ApiService) { }

  ngOnInit() {
    this.apiService.getHome().subscribe(
      (data) => {
        this.homeData = data;
      },
      (error) => {
        error = error;
        console.error('Failed to fetch home data', error);
      }
    );
  }
}
