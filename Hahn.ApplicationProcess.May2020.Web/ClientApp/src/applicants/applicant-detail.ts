import { ApplicantUpdated, ApplicantViewed } from './../common/messages';
import {inject} from 'aurelia-framework'
import {EventAggregator} from 'aurelia-event-aggregator'

import {ApplicantService} from "../services/services";
// import {areEqual} from './utility';

import { Router } from 'aurelia-router';

@inject(EventAggregator, ApplicantService, Router)
export class ApplicantDetail {
  private _applicantService: ApplicantService;
  event : any;
  router: Router;
  routeConfig : any;
  activeApplicant : any;
  originalApplicant : {};

  constructor(eventAggregator, applicantService: ApplicantService) {
      this.event = eventAggregator;
    this._applicantService = applicantService;
  }

  activate(params, routeConfig) {
    this.routeConfig = routeConfig;
    return this._applicantService.getApplicant(params.id)
      .then(applicant => {
        this.activeApplicant = applicant;
        this.routeConfig.navModel.setTitle(this.activeApplicant.name);
        this.originalApplicant = applicant;
        this.event.publish(new ApplicantViewed(this.activeApplicant));
      });
    }

  get canSave() {
    return this.activeApplicant.name && this.activeApplicant.familyName;
  }
  
  save() {
    this._applicantService.updateApplicant(this.activeApplicant.id, this.activeApplicant).then(applicant => {
      this.activeApplicant = applicant
      this.routeConfig.navModel.setTitle(this.activeApplicant.name);
      this.originalApplicant = applicant;
      this.event.publish(new ApplicantUpdated(this.activeApplicant));
      window.history.back();
    });
  }

  canDeactivate() {
    // if(!areEqual(this.originalApplicant, this.activeApplicant)){
    //   let result = confirm('You have unsaved changes. Are you sure you wish to leave?');
    //   if(!result) {
    //     this.event.publish(new ApplicantViewed(this.activeApplicant));
    //   }
    //   return result;
    // }
    // return true;
  }
}
