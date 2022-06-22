import { Component, OnDestroy, OnInit } from '@angular/core';
import {
  FormGroup,
  FormControl,
  FormArray,
  Validators,
  AbstractControl
} from '@angular/forms';
import { Router } from '@angular/router';
import {
  faCircleExclamation,
  faMagnifyingGlass,
  faPlus,
  faTrashCan
} from '@fortawesome/free-solid-svg-icons';
import { NgbTypeaheadSelectItemEvent } from '@ng-bootstrap/ng-bootstrap';
import { Store } from '@ngrx/store';
import {
  Subscription,
  OperatorFunction,
  Observable,
  combineLatest,
  debounceTime,
  switchMap,
  catchError,
  of,
  map
} from 'rxjs';
import {
  DepartmentAdmin,
  DepartmentCreate
} from 'src/app/models/department-create.model';
import { ProjectCreate } from 'src/app/models/project.model';
import { User } from 'src/app/models/user.model';
import { DepartmentService } from 'src/app/services/department.service';
import { EnterpriseService } from 'src/app/services/enterprise.service';
import { ProjectService } from 'src/app/services/project.service';
import { selectAppUser } from 'src/app/store/selectors/app-user.selector';
import { FormHelper } from 'src/app/util/form-helper';
import { DepartmentCodeValidator } from 'src/app/validators/department-code.validator';
import { textChangeRangeIsUnchanged } from 'typescript';

@Component({
  selector: 'app-project-create',
  templateUrl: './project-create.component.html',
  styleUrls: ['./project-create.component.scss']
})
export class ProjectCreateComponent implements OnInit, OnDestroy {
  warning = faCircleExclamation;
  glass = faMagnifyingGlass;
  plus = faPlus;
  trash = faTrashCan;

  private appUser$ = this.store.select(selectAppUser);
  private doNotJoinSubscription?: Subscription;
  private displayAsMemeberSubscription?: Subscription;
  private createNewDepartmentSubscription?: Subscription;

  private createDepartmentSubscription?: Subscription;

  private foundAdmin?: User;

  helper: FormHelper;
  createNewDepartment = false;

  constructor(
    private store: Store,
    private router: Router,
    private enterpriseService: EnterpriseService,
    private projectService: ProjectService,
    private departmentCodeValidator: DepartmentCodeValidator
  ) {
    this.helper = new FormHelper(this.createProjectForm);
  }

  departmentAdminFormGroup = new FormGroup({
    search: new FormControl(''),
    admins: new FormArray([])
  });

  attacheeDepartmentFormGroup = new FormGroup({
    search: new FormControl(''),
    id: new FormControl('')
  });

  createDepartmentFormGroup = new FormGroup({
    name: new FormControl('', [Validators.required]),
    code: new FormControl(
      '',
      [Validators.required],
      [this.departmentCodeValidator.validate.bind(this.departmentCodeValidator)]
    ),
    parentDepartment: new FormControl(''),
    admins: this.departmentAdminFormGroup,
    doNotJoin: new FormControl(false),
    displayAsMember: new FormControl(false)
  });

  createProjectForm = new FormGroup({
    name: new FormControl('', [Validators.required]),
    description: new FormControl(''),
    attacheeDepartment: this.attacheeDepartmentFormGroup,
    createNewDepartment: new FormControl(false),
    department: this.createDepartmentFormGroup
  });

  submitted = false;

  ngOnInit(): void {
    const doNotJoin = this.createDepartmentFormGroup.get('doNotJoin');
    const displayAsMember =
      this.createDepartmentFormGroup.get('displayAsMember');
    const createNewDepartment = this.createProjectForm.get(
      'createNewDepartment'
    );

    this.doNotJoinSubscription = doNotJoin?.valueChanges.subscribe(
      (value: boolean) => {
        console.log('doNotJoin', value);
        if (value) {
          displayAsMember?.disable({ emitEvent: false });
        } else {
          displayAsMember?.enable({ emitEvent: false });
        }
      }
    );
    this.displayAsMemeberSubscription = displayAsMember?.valueChanges.subscribe(
      (value: boolean) => {
        console.log('displayAsMemebr', value);

        if (value) {
          doNotJoin?.disable({ emitEvent: false });
        } else {
          doNotJoin?.enable({ emitEvent: false });
        }
      }
    );
    this.createNewDepartmentSubscription =
      createNewDepartment?.valueChanges.subscribe((value: boolean) => {
        console.log('createNewDepartment', value);

        this.createNewDepartment = value;
      });
  }

  ngOnDestroy(): void {
    this.doNotJoinSubscription?.unsubscribe();
    this.displayAsMemeberSubscription?.unsubscribe();
    this.createDepartmentSubscription?.unsubscribe();
    this.createNewDepartmentSubscription?.unsubscribe();
  }

  submit(): void {
    this.helper.submit();
    if (!this.createProjectForm.valid) {
      return;
    }

    const projectCreate: ProjectCreate = {
      name: this.createProjectForm.get('name')?.value,
      description: this.createProjectForm.get('description')?.value
    };

    if (this.createDepartmentFormGroup.get('name')?.value) {
      const admins: DepartmentAdmin[] = this.admins.controls.map(
        (adminControl: AbstractControl) => {
          const admin: DepartmentAdmin = {
            id: adminControl.get('id')?.value,
            displayAsMember: adminControl.get('displayAsMember')?.value
          };
          return admin;
        }
      );

      const departmentCreate: DepartmentCreate = {
        name: this.createDepartmentFormGroup.get('name')?.value,
        code: this.createDepartmentFormGroup.get('code')?.value,
        doNotJoin: this.createDepartmentFormGroup.get('doNotJoin')?.value,
        displayAsMember:
          this.createDepartmentFormGroup.get('displayAsMember')?.value,
        admins: admins
      };

      projectCreate.departmentCreate = departmentCreate;
    }

    this.createDepartmentSubscription = this.projectService
      .createProject(projectCreate)
      .pipe()
      .subscribe((department) => {
        if (department) {
          this.router.navigate(['/project', department.id, 'tasks']);
        }
      });
  }

  addAdmin(): void {
    const admins = this.createDepartmentFormGroup.get(
      'admins.admins'
    ) as FormArray;
    const search = this.createDepartmentFormGroup.get(
      'admins.search'
    ) as FormControl;
    if (!this.foundAdmin) {
      return;
    }

    admins.push(
      new FormGroup({
        id: new FormControl(this.foundAdmin.id),
        name: new FormControl({
          value: this.formatter(this.foundAdmin),
          disabled: true
        }),
        displayAsMember: new FormControl(false)
      })
    );
    this.foundAdmin = undefined;
    search.reset();
  }

  deleteAdmin(position: number): void {
    const admins = this.createDepartmentFormGroup.get(
      'admins.admins'
    ) as FormArray;
    admins.removeAt(position);
  }

  get admins(): FormArray {
    return this.createDepartmentFormGroup.get('admins.admins') as FormArray;
  }

  get adminNames(): string[] {
    return this.admins.controls.map(
      (control: AbstractControl) => control.get('name')?.value
    );
  }

  get adminIds(): number[] {
    return this.admins.controls.map(
      (control: AbstractControl) => control.get('id')?.value
    );
  }

  formatter = (user: User) => {
    if (!user) {
      return '';
    }
    let name = user.firstName;
    if (user.lastName) {
      name += ' ' + user.lastName;
    }
    name += ', ' + user.login;
    return name;
  };

  searchUsers: OperatorFunction<string, User[]> = (text$: Observable<string>) =>
    combineLatest([
      this.appUser$,
      text$.pipe(
        debounceTime(300),
        switchMap((text) => this.enterpriseService.searchEnterpriseUsers(text)),
        catchError(() => of([]))
      )
    ]).pipe(
      map(([appUser, users]) =>
        users.filter((user) => {
          if (!user) {
            return [];
          }
          return (
            user.id != appUser.userDetails.id &&
            this.adminIds?.indexOf(user.id) == -1
          );
        })
      )
    );

  selectAdmin(item: NgbTypeaheadSelectItemEvent): void {
    const user = item.item as User;
    this.foundAdmin = user;
  }
}
