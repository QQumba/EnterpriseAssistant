import { HttpClient } from '@angular/common/http';
import { Component, OnChanges, OnDestroy, OnInit } from '@angular/core';
import {
  AbstractControl,
  FormArray,
  FormControl,
  FormGroup,
  Validators
} from '@angular/forms';
import { Router } from '@angular/router';
import { text } from '@fortawesome/fontawesome-svg-core';
import {
  faCircleExclamation,
  faMagnifyingGlass,
  faPlus,
  faTrash,
  faTrashCan
} from '@fortawesome/free-solid-svg-icons';
import { NgbTypeaheadSelectItemEvent } from '@ng-bootstrap/ng-bootstrap';
import { Store } from '@ngrx/store';
import {
  catchError,
  combineLatest,
  debounceTime,
  distinctUntilChanged,
  iif,
  map,
  merge,
  Observable,
  of,
  OperatorFunction,
  skipWhile,
  Subscription,
  switchMap,
  takeWhile
} from 'rxjs';
import { AppUser } from 'src/app/models/appState/app-user.model';
import {
  DepartmentAdmin,
  DepartmentCreate
} from 'src/app/models/department-create.model';
import { User } from 'src/app/models/user.model';
import { DepartmentService } from 'src/app/services/department.service';
import { EnterpriseService } from 'src/app/services/enterprise.service';
import { selectAppUser } from 'src/app/store/selectors/app-user.selector';
import { FormHelper } from 'src/app/util/form-helper';
import { DepartmentCodeValidator } from 'src/app/validators/department-code.validator';

@Component({
  selector: 'app-department-create',
  templateUrl: './department-create.component.html',
  styleUrls: ['./department-create.component.scss']
})
export class DepartmentCreateComponent implements OnInit, OnDestroy {
  warning = faCircleExclamation;
  glass = faMagnifyingGlass;
  plus = faPlus;
  trash = faTrashCan;

  private appUser$ = this.store.select(selectAppUser);
  private doNotJoinSubscription?: Subscription;
  private displayAsMemeberSubscription?: Subscription;
  private createDepartmentSubscription?: Subscription;

  private foundAdmin?: User;

  helper: FormHelper;

  constructor(
    private store: Store,
    private router: Router,
    private enterpriseService: EnterpriseService,
    private departmentService: DepartmentService,
    private departmentCodeValidator: DepartmentCodeValidator
  ) {
    this.helper = new FormHelper(this.createDepartmentForm);
  }

  departmentAdminFormGroup = new FormGroup({
    search: new FormControl(''),
    admins: new FormArray([])
  });

  createDepartmentForm = new FormGroup({
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

  submitted = false;

  ngOnInit(): void {
    const doNotJoin = this.createDepartmentForm.get('doNotJoin');
    const displayAsMember = this.createDepartmentForm.get('displayAsMember');

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
  }

  ngOnDestroy(): void {
    this.doNotJoinSubscription?.unsubscribe();
    this.displayAsMemeberSubscription?.unsubscribe();
    this.createDepartmentSubscription?.unsubscribe();
  }

  submit(): void {
    this.helper.submit();
    if (!this.createDepartmentForm.valid) {
      return;
    }

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
      name: this.createDepartmentForm.get('name')?.value,
      code: this.createDepartmentForm.get('code')?.value,
      doNotJoin: this.createDepartmentForm.get('doNotJoin')?.value,
      displayAsMember: this.createDepartmentForm.get('displayAsMember')?.value,
      admins: admins
    };

    this.createDepartmentSubscription = this.departmentService
      .createDepartment(departmentCreate)
      .pipe()
      .subscribe((department) => {
        if (department) {
          this.router.navigate(['/department', department.id, 'users']);
        }
      });
  }

  addAdmin(): void {
    const admins = this.createDepartmentForm.get('admins.admins') as FormArray;
    const search = this.createDepartmentForm.get(
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
    const admins = this.createDepartmentForm.get('admins.admins') as FormArray;
    admins.removeAt(position);
  }

  get admins(): FormArray {
    return this.createDepartmentForm.get('admins.admins') as FormArray;
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
