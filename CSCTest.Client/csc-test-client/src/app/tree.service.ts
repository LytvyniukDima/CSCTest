import { Injectable } from '@angular/core';
import { environment } from '../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Organization } from './models/organization';
import { UrlBuilderService } from './url-builder.service';
import { Country } from './models/country';
import { Business } from './models/business';
import { Family } from './models/family';
import { Offering } from './models/offering';
import { Department } from './models/department';

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
    return this.httpClient.get<Country[]>(this.urlbuilder.build(this.cscTestApiUrl, 'organizations', organizationId.toString(), 'countries'));
  }

  getCountryBusinesses(countryId: number): Observable<Business[]> {
    return this.httpClient.get<Business[]>(this.urlbuilder.build(this.cscTestApiUrl, 'countries', countryId.toString(), 'businesses'));
  }

  getBusinessFamilies(businessId: number): Observable<Family[]> {
    return this.httpClient.get<Family[]>(this.urlbuilder.build(this.cscTestApiUrl, 'businesses', businessId.toString(), 'families'));
  }

  getFamilyOfferings(familyId: number): Observable<Offering[]> {
    return this.httpClient.get<Offering[]>(this.urlbuilder.build(this.cscTestApiUrl, 'families', familyId.toString(), 'offerings'));
  }

  getOfferingDepartments(offeringId: number): Observable<Department[]> {
    return this.httpClient.get<Department[]>(this.urlbuilder.build(this.cscTestApiUrl, 'offerings', offeringId.toString(), 'departments'));
  }
}
