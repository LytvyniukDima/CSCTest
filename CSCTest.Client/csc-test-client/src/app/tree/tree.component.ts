import { Component, OnInit, ViewChild } from '@angular/core';

import { TreeService } from '../tree.service';
import { Organization } from '../models/organization';
import { HttpErrorResponse } from '@angular/common/http';
import { Country } from '../models/country';
import { Observable } from 'rxjs';
import { of } from 'rxjs/observable/of';
import { Business } from '../models/business';
import { Family } from '../models/family';
import { Offering } from '../models/offering';
import { Department } from '../models/department';
import { DxTreeViewComponent } from 'devextreme-angular';

declare let require: any;

@Component({
  selector: 'app-tree',
  templateUrl: './tree.component.html',
  styleUrls: ['./tree.component.css']
})
export class TreeComponent implements OnInit {
  @ViewChild(DxTreeViewComponent) treeView: DxTreeViewComponent;
  
  organizationNameId = "organization_";
  countryNameId = "country_";
  businessNameId = "business_";
  familyNameId = "family_";
  offeringNameId = "offering_";
  departmentNameId = "department_";

  expandIndex = -1;

  constructor(private service: TreeService) {
  }

  startExpandAll() {
    this.expandIndex = 0;
    this.expandAll();
  }

  expandAll() {
    let nodes = this.treeView.items;
    for (var i = this.expandIndex; i < nodes.length; i++) {
      if (nodes[i].hasItems)
        this.treeView.instance.expandItem(nodes[i]);
    }
    this.expandIndex = nodes.length;
  }

  expandedItem(e) {
    if (this.expandIndex == -1)
      return;
    this.expandAll();
  }
  
  collapseAll() {
    var nodes = this.treeView.items;
    for (let node of nodes) {
      this.treeView.instance.collapseItem(node);
    }
  }

  createChildren = async (parent) => {
    if (parent === null)
      return await this.getOrganizations();
    if (parent.itemData === undefined)
      return;
    
    switch (parent.itemData.id) {
      case this.organizationNameId + parent.itemData.serverId: {
        return await this.getOrganizationCountries(parent.itemData.serverId);
      }
      case this.countryNameId + parent.itemData.serverId: {
        return await this.getCountryBusinesses(parent.itemData.serverId);
      }
      case this.businessNameId + parent.itemData.serverId: {
        return await this.getBusinessFamilies(parent.itemData.serverId);
      }
      case this.familyNameId + parent.itemData.serverId: {
        return await this.getFamilyOfferings(parent.itemData.serverId);
      }
      case this.offeringNameId + parent.itemData.serverId: {
        return await this.getOfferingDepartments(parent.itemData.serverId);
      }
    }
  }

  ngOnInit() {
    
  }

  async getOrganizations() {
    let organizations: Organization[];
    await this.service.getOrganizations()
      .toPromise()
      .then(data => {
        organizations = data;
      });

    return organizations.map((organization) => {
      return {
        id: this.organizationNameId + organization.id,
        parentId: "",
        text: organization.name,
        hasItems: organization.hasChildren,
        serverId: organization.id
      };
    });
  }

  async getOrganizationCountries(organizationId: number) {
    let countries: Country[];
    await this.service.getOrganizationCountries(organizationId)
      .toPromise()
      .then(data => {
        countries = data;
      });

    return countries.map((country) => {
      return {
        id: this.countryNameId + country.id,
        parentId: this.organizationNameId + country.organizationId,
        text: country.name,
        hasItems: country.hasChildren,
        serverId: country.id
      };
    });
  }

  async getCountryBusinesses(countryId: number) {
    let businesses: Business[];
    await this.service.getCountryBusinesses(countryId)
      .toPromise()
      .then(data => {
        businesses = data;
      });

    return businesses.map((business) => {
      return {
        id: this.businessNameId + business.id,
        parentId: this.countryNameId + business.countryId,
        text: business.name,
        hasItems: business.hasChildren,
        serverId: business.id
      };
    });
  }

  async getBusinessFamilies(businessId: number) {
    let families: Family[];
    await this.service.getBusinessFamilies(businessId)
      .toPromise()
      .then(data => {
        families = data;
      });

    return families.map((family) => {
      return {
        id: this.familyNameId + family.id,
        parentId: this.businessNameId + family.businessId,
        text: family.name,
        hasItems: family.hasChildren,
        serverId: family.id
      }
    })
  }

  async getFamilyOfferings(familyId: number) {
    let offerings: Offering[];
    await this.service.getFamilyOfferings(familyId)
      .toPromise()
      .then(data => {
        offerings = data;
      });

    return offerings.map((offering) => {
      return {
        id: this.offeringNameId + offering.id,
        parentId: this.familyNameId + offering.familyId,
        text: offering.name,
        hasItems: offering.hasChildren,
        serverId: offering.id
      }
    });
  }

  async getOfferingDepartments(offeringId: number) {
    let departments: Department[];
    await this.service.getOfferingDepartments(offeringId)
      .toPromise()
      .then(data => {
        departments = data;
      });
    return departments.map((department) => {
      return {
        id: this.departmentNameId + department.id,
        parentId: this.offeringNameId + department.offeringId,
        text: department.name,
        hasItems: false,
        serverId: department.id
      }
    });
  }
}
