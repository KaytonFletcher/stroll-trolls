//
//  MapViewController.swift
//  stroll-trolls
//
//  Created by Kayton Fletcher on 10/26/19.
//  Copyright © 2019 Kayton Fletcher. All rights reserved.
//

import UIKit
import Foundation
import CoreLocation
import MapboxCoreNavigation
import MapboxNavigation
import MapboxDirections

class MapViewController: UIViewController, MGLMapViewDelegate, CLLocationManagerDelegate {
    let locationManager = CLLocationManager()
    
//    override func viewDidLoad() {
//
//        let origin = CLLocationCoordinate2DMake(37.77440680146262, -122.43539772352648)
//        let destination = CLLocationCoordinate2DMake(37.76556957793795, -122.42409811526268)
//        let options = NavigationRouteOptions(coordinates: [origin, destination])
//
//        Directions.shared.calculate(options) { (waypoints, routes, error) in
//        guard let route = routes?.first, error == nil else {
//            print(error!.localizedDescription)
//        return
//        }
//
//        // For demonstration purposes, simulate locations if the Simulate Navigation option is on.
//        let navigationService = MapboxNavigationService(route: route, simulating: .onPoorGPS)
//        let navigationOptions = NavigationOptions(navigationService: navigationService)
//        let navigationViewController = NavigationViewController(for: route, options: navigationOptions)
//        navigationViewController.modalPresentationStyle = .fullScreen
//
//        self.present(navigationViewController, animated: true, completion: nil)
//        }
//    }
//
    
    
        override func viewDidLoad() {
            super.viewDidLoad()
            
            locationManager.requestAlwaysAuthorization()
            locationManager.requestWhenInUseAuthorization()
            if CLLocationManager.locationServicesEnabled() {
                locationManager.delegate = self
                locationManager.desiredAccuracy = kCLLocationAccuracyNearestTenMeters
                locationManager.startUpdatingLocation()
            }

            let mapView = MGLMapView(frame: view.bounds)
            mapView.autoresizingMask = [.flexibleWidth, .flexibleHeight]
            mapView.delegate = self

            mapView.styleURL = MGLStyle.outdoorsStyleURL
            
           

            // Mauna Kea, Hawaii
//            let center = CLLocationCoordinate2D(mapView.userLocation?.coordinate)
            if let loc = mapView.userLocation {
                // Optionally set a starting point.
                mapView.setCenter(loc.coordinate, zoomLevel: 7, direction: 0, animated: false)
            }
           

            view.addSubview(mapView)
            
            // Allow the map view to display the user's location
            mapView.showsUserLocation = true
        }
    
    func mapView(_ mapView: MGLMapView, annotationCanShowCallout annotation: MGLAnnotation) -> Bool {
        // Always allow callouts to popup when annotations are tapped.
        return true
    }


    func mapViewDidFinishLoadingMap(_ mapView: MGLMapView) {
        // Wait for the map to load before initiating the first camera movement.
        if let loc = mapView.userLocation {
               // Optionally set a starting point.
               mapView.setCenter(loc.coordinate, zoomLevel: 7, direction: 0, animated: false)
        }

        // Create a camera that rotates around the same center point, rotating 180°.
        // `fromDistance:` is meters above mean sea level that an eye would have to be in order to see what the map view is showing.
        let camera = MGLMapCamera(lookingAtCenter: mapView.centerCoordinate, altitude: 4500, pitch: 15, heading: 180)

        // Animate the camera movement over 5 seconds.
        mapView.setCamera(camera, withDuration: 5, animationTimingFunction: CAMediaTimingFunction(name: CAMediaTimingFunctionName.easeInEaseOut))
    }
}
