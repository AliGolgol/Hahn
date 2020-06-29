import {ApplicantViewed, ApplicantUpdated, ApplicantDeleted, ApplicantCreated} from '../common/messages'
import {ApplicantService} from "../services/services";

import {EventAggregator} from 'aurelia-event-aggregator'
import {inject} from 'aurelia-framework'

@inject(EventAggregator, ApplicantService)
export class ApplicantList {
  private _applicantService: ApplicantService;
  public message='Apllicant list';
  ea : any;
  applicants : [] | any;
  selectedId : number;

  constructor(ea: EventAggregator, ApplicantService: ApplicantService){
    this.ea = ea;
    this.applicants = [];
    this._applicantService = ApplicantService;

    ea.subscribe(ApplicantViewed, msg => this.select(msg.Applicant));

    ea.subscribe(ApplicantUpdated, msg => {
      let id = msg.Applicant.id;
      let found = this.applicants.find(x => x.id === id);
      Object.assign(found, msg.Applicant);
    });

    ea.subscribe(ApplicantDeleted, msg => {
      let deletedApplicant = msg.Applicant;
      this.applicants = this.applicants.filter(Applicant => Applicant !== deletedApplicant);
    });
    
    ea.subscribe(ApplicantCreated, msg => {
      let Applicant = msg.Applicant;
      this.applicants.push(Applicant);
    });
  }

  created() {
    this._applicantService.getApplicants()
      .then(data => this.applicants = data)
      .catch(err => console.log(err));
  }

  select(Applicant) {
    this.selectedId = Applicant.id;
    return true;
  }

  remove(applicant) {
    if(confirm('Are you sure that you want to delete this Applicant?')) {
      this._applicantService
        .deleteApplicant(applicant.id)
        .then(reponse => {
          this.ea.publish(new ApplicantDeleted(applicant))
        })
        .catch(err => console.log(err));
    }
  }
}
