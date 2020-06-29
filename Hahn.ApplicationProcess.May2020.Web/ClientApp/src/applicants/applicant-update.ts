import { inject, reset } from 'aurelia-framework';
import { EventAggregator } from "aurelia-event-aggregator";

import { ApplicantUpdated, ApplicantViewed } from '../common/messages'
import { ApplicantService } from '../services/services';

import { Router } from 'aurelia-router';
import { Applicant } from "../models/applicant";

@inject(EventAggregator, ApplicantService, Router)
export class ApplicantUpdate {
  private _applicantService: ApplicantService;
  event: any;
  router: Router;
  routeConfig: any;
  activeApplicant: any;
  originalApplicant: {};
  isValidate: boolean = false;
  valMsg;

  constructor(eventAggregator, applicantService: ApplicantService, router: Router) {
    this._applicantService = applicantService;
    this.event = eventAggregator;
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

  update() {
    this._applicantService
      .updateApplicant(this.activeApplicant.id, this.activeApplicant)
      .then(applicant => {
        this.activeApplicant = applicant
        this.routeConfig.navModel.setTitle(this.activeApplicant.name);
        this.originalApplicant = applicant;
        this.event.publish(new ApplicantUpdated(this.activeApplicant));
        window.history.back();

      }).catch(err => {
        this.isValidate = true;
        this.valMsg = err.response ? err.response.split(',') : "";
        window.history.back();

      });
  }

  reset() {
    window.history.back();
  }
}
