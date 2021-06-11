import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Subject } from 'rxjs';
import * as moment from 'moment';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent {
  public forecasts: WeatherForecast[];
  httpClient: HttpClient;
  baseUrl: string;
  moment = moment;
  targetElement: Element;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.httpClient = http;
    this.baseUrl = baseUrl;

    http.get<WeatherForecast[]>(baseUrl + 'weatherforecast').subscribe(result => {
      this.forecasts = result;
    }, error => console.error(error));
  }

  ngOnInit() {
    this.targetElement = document.querySelector('html');
  }

  refreshForecasts(event: Subject<any>) {
    this.forecasts = null;

    setTimeout(() => {
      this.httpClient.get<WeatherForecast[]>(this.baseUrl + 'weatherforecast').subscribe(result => {
        this.forecasts = result;
      }, error => console.error(error));
      event.next();
    }, 1000);
  }
}

interface WeatherForecast {
  applicable_date: Date;
  weather_state_name: string;
  weather_state_abbr: string;
}
