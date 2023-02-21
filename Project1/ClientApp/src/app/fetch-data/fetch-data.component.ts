import { Component, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent {
  public forecasts: WeatherForecast[] = [];
   fakeData = {
    firstName: 'John',
    lastName: 'Doe',
    age: 30
  };

  fakeFile = new File(['Test file content'], 'test.txt', { type: 'text/plain' });
  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    http.get<WeatherForecast[]>(baseUrl + 'weatherforecast').subscribe(result => {
      this.forecasts = result;
    }, error => console.error(error));
  }

  postTemplate1(data: any, file: File): Observable<any> {
    const formData = new FormData();
    formData.append('File', file, file.name);
    formData.append('data', JSON.stringify(data));
    const queryString = this.createQueryString({ "Test": "test", "tt": "tee" });
    return this.http.post<any>(this.baseUrl + 'weatherforecast', formData);
  }

  postTemplate() {
    this.postTemplate1(this.fakeData, this.fakeFile).subscribe(result => {
      console.log(result);
    }, error => console.error(error));
  }

  private createQueryString(obj: any): string {
    const queryString = Object.keys(obj)
      .map(key => `${encodeURIComponent(key)}=${encodeURIComponent(obj[key])}`)
      .join('&');
    return queryString ? `?${queryString}` : '';
  }
}

interface WeatherForecast {
  date: string;
  temperatureC: number;
  temperatureF: number;
  summary: string;
}
