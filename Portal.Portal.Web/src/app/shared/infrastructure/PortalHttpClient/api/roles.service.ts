/**
 * Demo API
 * No description provided (generated by Swagger Codegen https://github.com/swagger-api/swagger-codegen)
 *
 * OpenAPI spec version: v1
 * 
 *
 * NOTE: This class is auto generated by the swagger code generator program.
 * https://github.com/swagger-api/swagger-codegen.git
 * Do not edit the class manually.
 *//* tslint:disable:no-unused-variable member-ordering */

import { Inject, Injectable, Optional }                      from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams,
         HttpResponse, HttpEvent }                           from '@angular/common/http';
import { CustomHttpUrlEncodingCodec }                        from '../encoder';

import { Observable }                                        from 'rxjs';

import { AddEmployeeRequest } from '../model/addEmployeeRequest';
import { CreateRoleRequest } from '../model/createRoleRequest';
import { DeleteEmployeeRequest } from '../model/deleteEmployeeRequest';
import { EditRoleRequest } from '../model/editRoleRequest';
import { EmployeesResponseResult } from '../model/employeesResponseResult';
import { IEntityArrayResult } from '../model/iEntityArrayResult';
import { PermissionsResponseResult } from '../model/permissionsResponseResult';
import { RoleDetailsResponseResult } from '../model/roleDetailsResponseResult';
import { RolesListResponseResult } from '../model/rolesListResponseResult';

import { BASE_PATH, COLLECTION_FORMATS }                     from '../variables';
import { Configuration }                                     from '../configuration';


@Injectable()
export class RolesService {

    protected basePath = '/';
    public defaultHeaders = new HttpHeaders();
    public configuration = new Configuration();

    constructor(protected httpClient: HttpClient, @Optional()@Inject(BASE_PATH) basePath: string, @Optional() configuration: Configuration) {
        if (basePath) {
            this.basePath = basePath;
        }
        if (configuration) {
            this.configuration = configuration;
            this.basePath = basePath || configuration.basePath || this.basePath;
        }
    }

    /**
     * @param consumes string[] mime-types
     * @return true: consumes contains 'multipart/form-data', false: otherwise
     */
    private canConsumeForm(consumes: string[]): boolean {
        const form = 'multipart/form-data';
        for (const consume of consumes) {
            if (form === consume) {
                return true;
            }
        }
        return false;
    }


    /**
     * 
     * 
     * @param body 
     * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
     * @param reportProgress flag to report request and response progress.
     */
    public apiRolesAddemployeePost(body?: AddEmployeeRequest, observe?: 'body', reportProgress?: boolean): Observable<IEntityArrayResult>;
    public apiRolesAddemployeePost(body?: AddEmployeeRequest, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<IEntityArrayResult>>;
    public apiRolesAddemployeePost(body?: AddEmployeeRequest, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<IEntityArrayResult>>;
    public apiRolesAddemployeePost(body?: AddEmployeeRequest, observe: any = 'body', reportProgress: boolean = false ): Observable<any> {


        let headers = this.defaultHeaders;

        // authentication (Bearer) required
        if (this.configuration.apiKeys && this.configuration.apiKeys["Authorization"]) {
            headers = headers.set('Authorization', this.configuration.apiKeys["Authorization"]);
        }

        // to determine the Accept header
        let httpHeaderAccepts: string[] = [
            'text/plain',
            'application/json',
            'text/json'
        ];
        const httpHeaderAcceptSelected: string | undefined = this.configuration.selectHeaderAccept(httpHeaderAccepts);
        if (httpHeaderAcceptSelected != undefined) {
            headers = headers.set('Accept', httpHeaderAcceptSelected);
        }

        // to determine the Content-Type header
        const consumes: string[] = [
            'application/json',
            'text/json',
            'application/_*+json'
        ];
        const httpContentTypeSelected: string | undefined = this.configuration.selectHeaderContentType(consumes);
        if (httpContentTypeSelected != undefined) {
            headers = headers.set('Content-Type', httpContentTypeSelected);
        }

        return this.httpClient.request<IEntityArrayResult>('post',`${this.basePath}/api/roles/addemployee`,
            {
                body: body,
                withCredentials: this.configuration.withCredentials,
                headers: headers,
                observe: observe,
                reportProgress: reportProgress
            }
        );
    }

    /**
     * 
     * 
     * @param body 
     * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
     * @param reportProgress flag to report request and response progress.
     */
    public apiRolesCreaterolePost(body?: CreateRoleRequest, observe?: 'body', reportProgress?: boolean): Observable<IEntityArrayResult>;
    public apiRolesCreaterolePost(body?: CreateRoleRequest, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<IEntityArrayResult>>;
    public apiRolesCreaterolePost(body?: CreateRoleRequest, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<IEntityArrayResult>>;
    public apiRolesCreaterolePost(body?: CreateRoleRequest, observe: any = 'body', reportProgress: boolean = false ): Observable<any> {


        let headers = this.defaultHeaders;

        // authentication (Bearer) required
        if (this.configuration.apiKeys && this.configuration.apiKeys["Authorization"]) {
            headers = headers.set('Authorization', this.configuration.apiKeys["Authorization"]);
        }

        // to determine the Accept header
        let httpHeaderAccepts: string[] = [
            'text/plain',
            'application/json',
            'text/json'
        ];
        const httpHeaderAcceptSelected: string | undefined = this.configuration.selectHeaderAccept(httpHeaderAccepts);
        if (httpHeaderAcceptSelected != undefined) {
            headers = headers.set('Accept', httpHeaderAcceptSelected);
        }

        // to determine the Content-Type header
        const consumes: string[] = [
            'application/json',
            'text/json',
            'application/_*+json'
        ];
        const httpContentTypeSelected: string | undefined = this.configuration.selectHeaderContentType(consumes);
        if (httpContentTypeSelected != undefined) {
            headers = headers.set('Content-Type', httpContentTypeSelected);
        }

        return this.httpClient.request<IEntityArrayResult>('post',`${this.basePath}/api/roles/createrole`,
            {
                body: body,
                withCredentials: this.configuration.withCredentials,
                headers: headers,
                observe: observe,
                reportProgress: reportProgress
            }
        );
    }

    /**
     * 
     * 
     * @param body 
     * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
     * @param reportProgress flag to report request and response progress.
     */
    public apiRolesDeleteemployeePost(body?: DeleteEmployeeRequest, observe?: 'body', reportProgress?: boolean): Observable<IEntityArrayResult>;
    public apiRolesDeleteemployeePost(body?: DeleteEmployeeRequest, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<IEntityArrayResult>>;
    public apiRolesDeleteemployeePost(body?: DeleteEmployeeRequest, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<IEntityArrayResult>>;
    public apiRolesDeleteemployeePost(body?: DeleteEmployeeRequest, observe: any = 'body', reportProgress: boolean = false ): Observable<any> {


        let headers = this.defaultHeaders;

        // authentication (Bearer) required
        if (this.configuration.apiKeys && this.configuration.apiKeys["Authorization"]) {
            headers = headers.set('Authorization', this.configuration.apiKeys["Authorization"]);
        }

        // to determine the Accept header
        let httpHeaderAccepts: string[] = [
            'text/plain',
            'application/json',
            'text/json'
        ];
        const httpHeaderAcceptSelected: string | undefined = this.configuration.selectHeaderAccept(httpHeaderAccepts);
        if (httpHeaderAcceptSelected != undefined) {
            headers = headers.set('Accept', httpHeaderAcceptSelected);
        }

        // to determine the Content-Type header
        const consumes: string[] = [
            'application/json',
            'text/json',
            'application/_*+json'
        ];
        const httpContentTypeSelected: string | undefined = this.configuration.selectHeaderContentType(consumes);
        if (httpContentTypeSelected != undefined) {
            headers = headers.set('Content-Type', httpContentTypeSelected);
        }

        return this.httpClient.request<IEntityArrayResult>('post',`${this.basePath}/api/roles/deleteemployee`,
            {
                body: body,
                withCredentials: this.configuration.withCredentials,
                headers: headers,
                observe: observe,
                reportProgress: reportProgress
            }
        );
    }

    /**
     * 
     * 
     * @param body 
     * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
     * @param reportProgress flag to report request and response progress.
     */
    public apiRolesEditrolePost(body?: EditRoleRequest, observe?: 'body', reportProgress?: boolean): Observable<IEntityArrayResult>;
    public apiRolesEditrolePost(body?: EditRoleRequest, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<IEntityArrayResult>>;
    public apiRolesEditrolePost(body?: EditRoleRequest, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<IEntityArrayResult>>;
    public apiRolesEditrolePost(body?: EditRoleRequest, observe: any = 'body', reportProgress: boolean = false ): Observable<any> {


        let headers = this.defaultHeaders;

        // authentication (Bearer) required
        if (this.configuration.apiKeys && this.configuration.apiKeys["Authorization"]) {
            headers = headers.set('Authorization', this.configuration.apiKeys["Authorization"]);
        }

        // to determine the Accept header
        let httpHeaderAccepts: string[] = [
            'text/plain',
            'application/json',
            'text/json'
        ];
        const httpHeaderAcceptSelected: string | undefined = this.configuration.selectHeaderAccept(httpHeaderAccepts);
        if (httpHeaderAcceptSelected != undefined) {
            headers = headers.set('Accept', httpHeaderAcceptSelected);
        }

        // to determine the Content-Type header
        const consumes: string[] = [
            'application/json',
            'text/json',
            'application/_*+json'
        ];
        const httpContentTypeSelected: string | undefined = this.configuration.selectHeaderContentType(consumes);
        if (httpContentTypeSelected != undefined) {
            headers = headers.set('Content-Type', httpContentTypeSelected);
        }

        return this.httpClient.request<IEntityArrayResult>('post',`${this.basePath}/api/roles/editrole`,
            {
                body: body,
                withCredentials: this.configuration.withCredentials,
                headers: headers,
                observe: observe,
                reportProgress: reportProgress
            }
        );
    }

    /**
     * 
     * 
     * @param search 
     * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
     * @param reportProgress flag to report request and response progress.
     */
    public apiRolesEmployeesGet(search?: string, observe?: 'body', reportProgress?: boolean): Observable<EmployeesResponseResult>;
    public apiRolesEmployeesGet(search?: string, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<EmployeesResponseResult>>;
    public apiRolesEmployeesGet(search?: string, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<EmployeesResponseResult>>;
    public apiRolesEmployeesGet(search?: string, observe: any = 'body', reportProgress: boolean = false ): Observable<any> {


        let queryParameters = new HttpParams({encoder: new CustomHttpUrlEncodingCodec()});
        if (search !== undefined && search !== null) {
            queryParameters = queryParameters.set('search', <any>search);
        }

        let headers = this.defaultHeaders;

        // authentication (Bearer) required
        if (this.configuration.apiKeys && this.configuration.apiKeys["Authorization"]) {
            headers = headers.set('Authorization', this.configuration.apiKeys["Authorization"]);
        }

        // to determine the Accept header
        let httpHeaderAccepts: string[] = [
            'text/plain',
            'application/json',
            'text/json'
        ];
        const httpHeaderAcceptSelected: string | undefined = this.configuration.selectHeaderAccept(httpHeaderAccepts);
        if (httpHeaderAcceptSelected != undefined) {
            headers = headers.set('Accept', httpHeaderAcceptSelected);
        }

        // to determine the Content-Type header
        const consumes: string[] = [
        ];

        return this.httpClient.request<EmployeesResponseResult>('get',`${this.basePath}/api/roles/employees`,
            {
                params: queryParameters,
                withCredentials: this.configuration.withCredentials,
                headers: headers,
                observe: observe,
                reportProgress: reportProgress
            }
        );
    }

    /**
     * 
     * 
     * @param basedOnRoleId 
     * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
     * @param reportProgress flag to report request and response progress.
     */
    public apiRolesPermissionsGet(basedOnRoleId?: number, observe?: 'body', reportProgress?: boolean): Observable<PermissionsResponseResult>;
    public apiRolesPermissionsGet(basedOnRoleId?: number, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<PermissionsResponseResult>>;
    public apiRolesPermissionsGet(basedOnRoleId?: number, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<PermissionsResponseResult>>;
    public apiRolesPermissionsGet(basedOnRoleId?: number, observe: any = 'body', reportProgress: boolean = false ): Observable<any> {


        let queryParameters = new HttpParams({encoder: new CustomHttpUrlEncodingCodec()});
        if (basedOnRoleId !== undefined && basedOnRoleId !== null) {
            queryParameters = queryParameters.set('BasedOnRoleId', <any>basedOnRoleId);
        }

        let headers = this.defaultHeaders;

        // authentication (Bearer) required
        if (this.configuration.apiKeys && this.configuration.apiKeys["Authorization"]) {
            headers = headers.set('Authorization', this.configuration.apiKeys["Authorization"]);
        }

        // to determine the Accept header
        let httpHeaderAccepts: string[] = [
            'text/plain',
            'application/json',
            'text/json'
        ];
        const httpHeaderAcceptSelected: string | undefined = this.configuration.selectHeaderAccept(httpHeaderAccepts);
        if (httpHeaderAcceptSelected != undefined) {
            headers = headers.set('Accept', httpHeaderAcceptSelected);
        }

        // to determine the Content-Type header
        const consumes: string[] = [
        ];

        return this.httpClient.request<PermissionsResponseResult>('get',`${this.basePath}/api/roles/permissions`,
            {
                params: queryParameters,
                withCredentials: this.configuration.withCredentials,
                headers: headers,
                observe: observe,
                reportProgress: reportProgress
            }
        );
    }

    /**
     * 
     * 
     * @param roleId 
     * @param search 
     * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
     * @param reportProgress flag to report request and response progress.
     */
    public apiRolesRoledetailsGet(roleId: number, search?: string, observe?: 'body', reportProgress?: boolean): Observable<RoleDetailsResponseResult>;
    public apiRolesRoledetailsGet(roleId: number, search?: string, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<RoleDetailsResponseResult>>;
    public apiRolesRoledetailsGet(roleId: number, search?: string, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<RoleDetailsResponseResult>>;
    public apiRolesRoledetailsGet(roleId: number, search?: string, observe: any = 'body', reportProgress: boolean = false ): Observable<any> {

        if (roleId === null || roleId === undefined) {
            throw new Error('Required parameter roleId was null or undefined when calling apiRolesRoledetailsGet.');
        }


        let queryParameters = new HttpParams({encoder: new CustomHttpUrlEncodingCodec()});
        if (roleId !== undefined && roleId !== null) {
            queryParameters = queryParameters.set('RoleId', <any>roleId);
        }
        if (search !== undefined && search !== null) {
            queryParameters = queryParameters.set('Search', <any>search);
        }

        let headers = this.defaultHeaders;

        // authentication (Bearer) required
        if (this.configuration.apiKeys && this.configuration.apiKeys["Authorization"]) {
            headers = headers.set('Authorization', this.configuration.apiKeys["Authorization"]);
        }

        // to determine the Accept header
        let httpHeaderAccepts: string[] = [
            'text/plain',
            'application/json',
            'text/json'
        ];
        const httpHeaderAcceptSelected: string | undefined = this.configuration.selectHeaderAccept(httpHeaderAccepts);
        if (httpHeaderAcceptSelected != undefined) {
            headers = headers.set('Accept', httpHeaderAcceptSelected);
        }

        // to determine the Content-Type header
        const consumes: string[] = [
        ];

        return this.httpClient.request<RoleDetailsResponseResult>('get',`${this.basePath}/api/roles/roledetails`,
            {
                params: queryParameters,
                withCredentials: this.configuration.withCredentials,
                headers: headers,
                observe: observe,
                reportProgress: reportProgress
            }
        );
    }

    /**
     * 
     * 
     * @param search 
     * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
     * @param reportProgress flag to report request and response progress.
     */
    public apiRolesRoleslistGet(search?: string, observe?: 'body', reportProgress?: boolean): Observable<RolesListResponseResult>;
    public apiRolesRoleslistGet(search?: string, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<RolesListResponseResult>>;
    public apiRolesRoleslistGet(search?: string, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<RolesListResponseResult>>;
    public apiRolesRoleslistGet(search?: string, observe: any = 'body', reportProgress: boolean = false ): Observable<any> {


        let queryParameters = new HttpParams({encoder: new CustomHttpUrlEncodingCodec()});
        if (search !== undefined && search !== null) {
            queryParameters = queryParameters.set('Search', <any>search);
        }

        let headers = this.defaultHeaders;

        // authentication (Bearer) required
        if (this.configuration.apiKeys && this.configuration.apiKeys["Authorization"]) {
            headers = headers.set('Authorization', this.configuration.apiKeys["Authorization"]);
        }

        // to determine the Accept header
        let httpHeaderAccepts: string[] = [
            'text/plain',
            'application/json',
            'text/json'
        ];
        const httpHeaderAcceptSelected: string | undefined = this.configuration.selectHeaderAccept(httpHeaderAccepts);
        if (httpHeaderAcceptSelected != undefined) {
            headers = headers.set('Accept', httpHeaderAcceptSelected);
        }

        // to determine the Content-Type header
        const consumes: string[] = [
        ];

        return this.httpClient.request<RolesListResponseResult>('get',`${this.basePath}/api/roles/roleslist`,
            {
                params: queryParameters,
                withCredentials: this.configuration.withCredentials,
                headers: headers,
                observe: observe,
                reportProgress: reportProgress
            }
        );
    }

}