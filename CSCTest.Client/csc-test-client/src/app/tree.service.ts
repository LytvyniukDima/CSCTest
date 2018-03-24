import { Injectable } from '@angular/core';
import { environment } from '../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Organization } from './models/organization';
import { UrlBuilderService } from './url-builder.service';
import { Country } from './models/country';

@Injectable()
export class TreeService {
  private readonly cscTestApiUrl: string;

  constructor(private httpClient: HttpClient, private urlbuilder: UrlBuilderService) {
    this.cscTestApiUrl = environment['CSCTestUrl'];
  }

  getOrganizations(): Observable<Organization[]> {
    return this.httpClient.get<Organization[]>(this.urlbuilder.build(this.cscTestApiUrl, 'organizations'));
  }

  getOrganizationCountries(organizationId: number): Observable<Country[]> {
    return this.httpClient.get<Country[]>(this.urlbuilder.build(this.cscTestApiUrl, 'organizations',organizationId.toString(),'countries'));
  }
}
