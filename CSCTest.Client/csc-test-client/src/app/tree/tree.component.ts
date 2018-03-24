import { Component, OnInit } from '@angular/core';

import { TreeService } from '../tree.service';
import { Organization } from '../models/organization';
import { HttpErrorResponse } from '@angular/common/http';
import { Country } from '../models/country';
import { Observable } from 'rxjs';
import { of } from 'rxjs/observable/of';

declare let require: any;

@Component({
  selector: 'app-tree',
  templateUrl: './tree.component.html',
  styleUrls: ['./tree.component.css']
})
export class TreeComponent implements OnInit {

  constructor(private service: TreeService) {
  }

  selectItem(e) {
    
  }

  createChildren = async (parent) => {
    return await this.getOrganizations();
  }

  ngOnInit() {

  }

  async getOrganizations() {
    let organizations;
    await this.service.getOrganizations()
      .toPromise()
      .then(data => { 
        organizations = data;
      });

    return organizations.map((organization) => {
      return {
        id: organization.id,
        parentId: "",
        text: organization.text,
        hasChildren: organization.hasChildren
      };
    });
  }

  getOrganizationCountries(organization: Organization) {

  }

}
